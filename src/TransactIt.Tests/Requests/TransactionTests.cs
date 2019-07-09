using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.Transactions;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class TransactionTests
    {
        private NoTrackingContext _noTrackingContext;
        private TrackingContext _trackingContext;

        [TestInitialize]
        public void Initialize()
        {
            var inMemoryDatabaseReference = Guid.NewGuid().ToString();

            var optionsNoTrackingContext = new DbContextOptionsBuilder<NoTrackingContext>().UseInMemoryDatabase(inMemoryDatabaseReference).Options;
            _noTrackingContext = new NoTrackingContext(optionsNoTrackingContext);

            var optionsTrackingContext = new DbContextOptionsBuilder<TrackingContext>().UseInMemoryDatabase(inMemoryDatabaseReference).Options;
            _trackingContext = new TrackingContext(optionsTrackingContext);

            Mapper.Initialize(x =>
            {
                x.AddProfile<Infrastructure.Profiles.MainAccountGroupProfile>();
                x.AddProfile<Infrastructure.Profiles.SubAccountGroupProfile>();
                x.AddProfile<Infrastructure.Profiles.AccountProfile>();
                x.AddProfile<Infrastructure.Profiles.TransactionProfile>();
                x.AddProfile<Infrastructure.Profiles.AccountingEntryProfile>();
            });
        }

        [TestMethod]
        public async Task SaveTransaction_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult1 = _trackingContext.AddTestData<Domain.Entities.Ledger>(expectedResultCount);
            var dataGenerationResult2 = _trackingContext.AddTestData<Domain.Entities.Transaction>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult1.Item1);
            Assert.IsTrue(dataGenerationResult2.Item1);

            var ledgerId = dataGenerationResult1.Item2[0];

            var model = new Domain.Models.Transaction
            {
                Description = "Test",
                TransactionDate = DateTime.UtcNow,
                AccountingEntries = new List<Domain.Models.AccountingEntry>
                {
                    new Domain.Models.AccountingEntry
                    {
                        Amount = 100,
                        Side = Domain.Models.AccountingEntry.EntrySide.Credit,
                        AccountId = 1
                    },
                    new Domain.Models.AccountingEntry
                    {
                        Amount = 100,
                        Side = Domain.Models.AccountingEntry.EntrySide.Debit,
                        AccountId = 1
                    }
                }
            };
            var request = new SaveTransactionRequest(ledgerId, model);
            var handler = new SaveTransactionRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Ledger\" (666) was not found")]
        public async Task SaveFinancialTransaction_Failure_NoParentLedger()
        {
            var ledgerId = 666;
            var model = new Domain.Models.Transaction
            {
                Description = "Test",
                TransactionDate = DateTime.UtcNow,
                AccountingEntries = new List<Domain.Models.AccountingEntry>
                {
                    new Domain.Models.AccountingEntry { Amount = 100, Side = Domain.Models.AccountingEntry.EntrySide.Credit},
                    new Domain.Models.AccountingEntry { Amount = 100, Side = Domain.Models.AccountingEntry.EntrySide.Debit}
                }
            };
            var request = new SaveTransactionRequest(ledgerId, model);
            var handler = new SaveTransactionRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        [DataTestMethod]
        [DataRow(
            Domain.Models.AccountingEntry.EntrySide.Debit,
            100,
            Domain.Models.AccountingEntry.EntrySide.Debit,
            100,
            false)]
        [DataRow(
            Domain.Models.AccountingEntry.EntrySide.Debit,
            100,
            Domain.Models.AccountingEntry.EntrySide.Credit,
            100,
            true)]
        [DataRow(
            Domain.Models.AccountingEntry.EntrySide.Debit,
            100,
            Domain.Models.AccountingEntry.EntrySide.Credit,
            90,
            false)]
        public async Task SaveFinancialTransaction_DebitCreditSum_Validation(
            Domain.Models.AccountingEntry.EntrySide side1,
            int amount1,
            Domain.Models.AccountingEntry.EntrySide side2,
            int amount2,
            bool isValid)
        {
            var ledgerId = 666;
            var model = new Domain.Models.Transaction
            {
                Description = "Test",
                TransactionDate = DateTime.UtcNow,
                AccountingEntries = new List<Domain.Models.AccountingEntry>
                {
                    new Domain.Models.AccountingEntry { Amount = amount1, Side = side1, AccountId = 1},
                    new Domain.Models.AccountingEntry { Amount = amount2, Side = side2, AccountId = 2}
                }
            };

            var request = new SaveTransactionRequest(ledgerId, model);
            var validator = new SaveTransactionValidator();
            var validationResult = await validator.ValidateAsync(request);
            Assert.AreEqual(isValid, validationResult.IsValid);
        }



        [TestCleanup]
        public void CleanUp()
        {
            _trackingContext.Dispose();
            _noTrackingContext.Dispose();
            Mapper.Reset();
        }
    }
}
