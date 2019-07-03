﻿using AutoMapper;
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




        [TestCleanup]
        public void CleanUp()
        {
            _trackingContext.Dispose();
            _noTrackingContext.Dispose();
            Mapper.Reset();
        }
    }
}
