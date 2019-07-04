using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.FinancialTransactions;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class FinancialTransactionTests
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
                x.AddProfile<Infrastructure.Profiles.LedgerMainAccountGroupProfile>();
                x.AddProfile<Infrastructure.Profiles.LedgerSubAccountGroupProfile>();
                x.AddProfile<Infrastructure.Profiles.LedgerAccountProfile>();
                x.AddProfile<Infrastructure.Profiles.FinancialTransactionProfile>();
                x.AddProfile<Infrastructure.Profiles.AccountingEntryProfile>();
            });
        }

        [TestMethod]
        public async Task SaveFinancialTransaction_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult1 = _trackingContext.AddTestData<Domain.Entities.Ledger>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult1.Item1);

            var ledgerId = dataGenerationResult1.Item2[0];

            var model = new Domain.Models.FinancialTransaction
            {
                IdentifyingCode = 1,
                TransactionDate = DateTime.UtcNow,
                AccountingEntries = new List<Domain.Models.AccountingEntry>
                {
                    new Domain.Models.AccountingEntry
                    {
                        Amount = 100,
                        Side = Domain.Models.AccountingEntry.EntrySide.Credit,
                        LedgerAccountId = 1
                    },
                    new Domain.Models.AccountingEntry
                    {
                        Amount = 100,
                        Side = Domain.Models.AccountingEntry.EntrySide.Debit,
                        LedgerAccountId = 1
                    }
                }
            };
            var request = new SaveFinancialTransactionRequest(ledgerId, model);
            var handler = new SaveFinancialTransactionRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Ledger\" (666) was not found")]
        public async Task SaveFinancialTransaction_Failure_NoParentLedger()
        {
            var ledgerId = 666;
            var model = new Domain.Models.FinancialTransaction
            {
                IdentifyingCode = 1,
                TransactionDate = DateTime.UtcNow,
                AccountingEntries = new List<Domain.Models.AccountingEntry>
                {
                    new Domain.Models.AccountingEntry { Amount = 100, Side = Domain.Models.AccountingEntry.EntrySide.Credit},
                    new Domain.Models.AccountingEntry { Amount = 100, Side = Domain.Models.AccountingEntry.EntrySide.Debit}
                }
            };
            var request = new SaveFinancialTransactionRequest(ledgerId, model);
            var handler = new SaveFinancialTransactionRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        //TODO: Add validation tests



        [TestCleanup]
        public void CleanUp()
        {
            _trackingContext.Dispose();
            _noTrackingContext.Dispose();
            Mapper.Reset();
        }
    }
}
