using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Read.Ledgers;
using TransactIt.Data.Contexts;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class LedgerTests
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

            Mapper.Initialize(x => x.AddProfile<Infrastructure.Profiles.LedgerProfile>());
        }

        [TestMethod]
        public void ProfileConfiguration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public async Task FindAllLedgers_Success()
        {
            var expectedResultCount = 5;

            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.Ledger>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult.Item1);

            var handler = new FindAllLedgersRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(new FindAllLedgersRequest(), default(CancellationToken));

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResultCount, result.Count);
        }

        [TestMethod]
        public async Task FindLedgerById_Success()
        {
            var dataGenerationResult = _noTrackingContext.AddTestData<Domain.Entities.Ledger>(1);
            Assert.IsTrue(dataGenerationResult.Item1);

            var selectedId = dataGenerationResult.Item2[0];
            var request = new FindLedgerByIdRequest(selectedId);

            var handler = new FindLedgerByIdRequestHandler(_noTrackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.IsNotNull(result);
            Assert.AreEqual(selectedId, result.Id);
        }

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(3243243, true)]
        public async Task FindLedgerById_Validation(int id, bool isValid)
        {
            var request = new FindLedgerByIdRequest(id);

            var validator = new FindLedgerByIdValidator();
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
