using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.LedgerSubAccountGroups;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class LedgerSubAccountGroupTests
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
        public async Task SaveSubLedgerAccountGroup_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult1 = _trackingContext.AddTestData<Domain.Entities.Ledger>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult1.Item1);

            var ledgerId = dataGenerationResult1.Item2[0];

            var dataGenerationResult2 = _trackingContext.AddTestData<Domain.Entities.LedgerMainAccountGroup>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult2.Item1);

            var ledgerMainAccountGroupId = dataGenerationResult2.Item2[0];

            var model = new Domain.Models.LedgerSubAccountGroup { Number = 19, Name = "Kassa och bank" };
            var request = new SaveLedgerSubAccountGroupRequest(
                ledgerId,
                ledgerMainAccountGroupId,
                model);

            var handler = new SaveLedgerSubAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Ledger main account group\" (666) was not found")]
        public async Task SaveSubLedgerAccountGroup_Failure_NoParentMainAccountGroup()
        {
            var ledgerId = 1;
            var ledgerMainAccountGroupId = 2;
            var model = new Domain.Models.LedgerSubAccountGroup { Number = 19, Name = "Kassa och bank"};
            var request = new SaveLedgerSubAccountGroupRequest(ledgerId, ledgerMainAccountGroupId, model);

            var handler = new SaveLedgerSubAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        [DataTestMethod]
        [DataRow("", 19, 1, 1, true, false)]
        [DataRow("Test", 0, 1, 1, true, false)]
        [DataRow("Test", 19, 0, 1, true, false)]
        [DataRow("Test", 19, 1, 0, true, false)]
        [DataRow("Test", 19, 1, 1, true, true)]
        [DataRow(null, 0, 1, 1, false, false)]
        [DataRow(null, 0, 1, 1, true, false)]
        public async Task SaveLedgerSubAccountGroup_Validation(
            string ledgerSubAccountGroupName,
            int ledgerSubAccountGroupNumber,
            int ledgerId,
            int ledgerMainAccountGroupId,
            bool instantiateModel,
            bool isValid)
        {
            Domain.Models.LedgerSubAccountGroup model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.LedgerSubAccountGroup {
                    Number = ledgerSubAccountGroupNumber,
                    Name = ledgerSubAccountGroupName
                };
            }
            var request = new SaveLedgerSubAccountGroupRequest(ledgerId, ledgerMainAccountGroupId, model);
            var validator = new SaveLedgerSubAccountGroupValidator();
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
