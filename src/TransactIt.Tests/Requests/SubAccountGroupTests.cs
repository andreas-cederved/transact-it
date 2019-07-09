using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransactIt.Application.Write.SubAccountGroups;
using TransactIt.Data.Contexts;
using TransactIt.Intersection.Exceptions;
using TransactIt.Tests.Extensions;

namespace TransactIt.Tests.Requests
{
    [TestClass]
    public class SubAccountGroupTests
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
        public async Task SaveSubAccountGroup_Success()
        {
            var expectedResultCount = 1;

            var dataGenerationResult1 = _trackingContext.AddTestData<Domain.Entities.MainAccountGroup>(expectedResultCount);
            Assert.IsTrue(dataGenerationResult1.Item1);

            var mainAccountGroupId = dataGenerationResult1.Item2[0];

            var model = new Domain.Models.SubAccountGroup { Number = 19, Name = "Kassa och bank" };
            var request = new SaveSubAccountGroupRequest(mainAccountGroupId, model);

            var handler = new SaveSubAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
            Assert.AreEqual(result, Unit.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException), "Entity \"Main account group\" (666) was not found")]
        public async Task SaveSubAccountGroup_Failure_NoParentMainAccountGroup()
        {
            var mainAccountGroupId = 666;
            var model = new Domain.Models.SubAccountGroup { Number = 19, Name = "Kassa och bank"};
            var request = new SaveSubAccountGroupRequest(mainAccountGroupId, model);

            var handler = new SaveSubAccountGroupRequestHandler(_trackingContext, Mapper.Instance);
            var result = await handler.Handle(request, default(CancellationToken));
        }

        [DataTestMethod]
        [DataRow("", 19, 1, true, false)]
        [DataRow("Test", 1, 0, true, false)]
        [DataRow("Test", 19, 1, true, true)]
        [DataRow(null, 1, 1, false, false)]
        [DataRow(null, 1, 1, true, false)]
        public async Task SaveLedgerSubAccountGroup_Validation(
            string subAccountGroupName,
            int subAccountGroupNumber,
            int mainAccountGroupId,
            bool instantiateModel,
            bool isValid)
        {
            Domain.Models.SubAccountGroup model = null;
            if (instantiateModel)
            {
                model = new Domain.Models.SubAccountGroup {
                    Number = subAccountGroupNumber,
                    Name = subAccountGroupName
                };
            }
            var request = new SaveSubAccountGroupRequest(mainAccountGroupId, model);
            var validator = new SaveSubAccountGroupValidator();
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
