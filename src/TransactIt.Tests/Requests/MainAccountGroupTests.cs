using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.MainAccountGroups;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class MainAccountGroupTests
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
                x.AddProfile<Infrastructure.Profiles.AccountingEntryProfile>();
            });
        }

        [TestMethod]
        public async Task SaveMainAccountGroup_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult = _trackingContext.AddTestData<Domain.Entities.Ledger>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult.Item1);

            var ledgerId = dataGenerationResult.Item2[0];
            var model = new Domain.Models.MainAccountGroup { Number = 1, Name = "Tillgångar" };
            var request = new SaveMainAccountGroupRequest(ledgerId, model);

            var handler = new SaveMainAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Ledger\" (666) was not found")]
        public async Task SaveMainAccountGroup_Failure_NoParentLedger()
        {
            var ledgerId = 666;
            var model = new Domain.Models.MainAccountGroup { Number = 3000, Name = "TestLedger", Description = "TestLedger description" };
            var request = new SaveMainAccountGroupRequest(ledgerId, model);

            var handler = new SaveMainAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        [DataTestMethod]
        [DataRow("", 1, 1, true, false)]
        [DataRow("Test", 0, 1, true, false)]
        [DataRow("Test", 3000, 1, true, true)]
        [DataRow(null, 0, 1, false, false)]
        [DataRow(null, 0, 1, true, false)]
        public async Task SaveMainAccountGroup_Validation(
            string mainAccountGroupName,
            int mainAccountGroupNumber,
            int ledgerId,
            bool instantiateModel,
            bool isValid)
        {
            Domain.Models.MainAccountGroup model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.MainAccountGroup {
                    Number = mainAccountGroupNumber,
                    Name = mainAccountGroupName
                };
            }
            var request = new SaveMainAccountGroupRequest(ledgerId, model);
            var validator = new SaveMainAccountGroupValidator();
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
