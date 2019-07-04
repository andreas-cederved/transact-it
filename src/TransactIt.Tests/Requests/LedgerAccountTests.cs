using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.LedgerAccounts;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class LedgerAccountTests
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
                x.AddProfile<Infrastructure.Profiles.AccountingEntryProfile>();
            });
        }

        [TestMethod]
        public async Task SaveLedgerAccount_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult1 = _trackingContext.AddTestData<Domain.Entities.LedgerSubAccountGroup>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult1.Item1);

            var ledgerSubAccountGroupId = dataGenerationResult1.Item2[0];

            var model = new Domain.Models.LedgerAccount { Number = 1931, Name = "Företagskonto" };
            var request = new SaveLedgerAccountRequest(ledgerSubAccountGroupId, model);

            var handler = new SaveLedgerAccountRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Ledger sub account group\" (666) was not found")]
        public async Task SaveLedgerAccount_Failure_NoParentSubAccountGroup()
        {
            var ledgerSubAccountGroupId = 666;
            var model = new Domain.Models.LedgerAccount { Number = 1931, Name = "Företagskonto" };
            var request = new SaveLedgerAccountRequest(ledgerSubAccountGroupId, model);

            var handler = new SaveLedgerAccountRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        [DataTestMethod]
        [DataRow("", 1931, 1, true, false)]
        [DataRow("Test", 0, 1, true, false)]
        [DataRow("Test", 1931, 0, true, false)]
        [DataRow("Test", 1931, 1, true, true)]
        [DataRow(null, 0, 1, false, false)]
        [DataRow(null, 0, 1, true, false)]
        public async Task SaveLedgerAccount_Validation(
            string ledgerAccountName,
            int ledgerAccountNumber,
            int ledgerSubAccountGroupId,
            bool instantiateModel,
            bool isValid)
        {
            Domain.Models.LedgerAccount model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.LedgerAccount
                {
                    Number = ledgerAccountNumber,
                    Name = ledgerAccountName
                };
            }
            var request = new SaveLedgerAccountRequest(ledgerSubAccountGroupId, model);
            var validator = new SaveLedgerAccountValidator();
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
