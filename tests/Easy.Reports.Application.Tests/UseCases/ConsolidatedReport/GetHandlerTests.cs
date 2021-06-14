using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.UseCases.ConsolidatedReport
{
    [Collection(nameof(GetHandlerCollection))]
    public class GetHandlerTests
    {
        private readonly GetHandlerTestsFixture _getHandlerTestsFixture;
        private readonly GetHandler _getHandler;
        private readonly GetQuery _getQuery;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        public GetHandlerTests(GetHandlerTestsFixture getHandlerTestsFixture)
        {
            _getHandlerTestsFixture = getHandlerTestsFixture;
            _getHandler = _getHandlerTestsFixture.CreateInstanceGetHandler();
            _getQuery = _getHandlerTestsFixture.GenerateGetQuery();
        }

        [Fact(DisplayName = "Get Consolidated Report Ok")]
        [Trait("Category", "UseCase - ConsolidatedReport - Get")]
        public async Task ConsolidateReport_Get_MustGetOk()
        {
            // Arrange
            var investments = _getHandlerTestsFixture.GenerateInvestmentsOk();
            var investmentsFirst = investments.FirstOrDefault();

            #region mocking IMemoryCache
            string keyPayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentRepository>()
                .Setup(c => c.GetAllCalculatedInvestmentsAsync(_getQuery.RescueDate))
                .ReturnsAsync(investments);

            // Act
            var result = await _getHandler.Handle(_getQuery, _cancellationToken);
            var resultInvestmentsListFirst = result.Investments.ToList().FirstOrDefault();

            // Assert
            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentRepository>().Verify(c => c.GetAllCalculatedInvestmentsAsync(_getQuery.RescueDate), Times.Once);
            Assert.Equal(investmentsFirst.InvestedValue, resultInvestmentsListFirst.InvestedValue);
            Assert.Equal(investmentsFirst.TotalValue, resultInvestmentsListFirst.TotalValue);
            Assert.Equal(investmentsFirst.DueDate, resultInvestmentsListFirst.DueDate);
            Assert.Equal(investmentsFirst.PurchaseDate, resultInvestmentsListFirst.PurchaseDate);
            Assert.Equal(investmentsFirst.Name, resultInvestmentsListFirst.Name);
        }

        [Fact(DisplayName = "Get Consolidated Report Not Ok")]
        [Trait("Category", "UseCase - ConsolidatedReport - Get")]
        public async Task ConsolidateReport_Get_MustGetNotOk()
        {
            // Arrange
            var investments = _getHandlerTestsFixture.GenerateInvestmentsNotOk();

            #region mocking IMemoryCache
            string keyPayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _getHandlerTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentRepository>()
                .Setup(c => c.GetAllCalculatedInvestmentsAsync(_getQuery.RescueDate))
                .ReturnsAsync(investments);

            // Act
            var result = await _getHandler.Handle(_getQuery, _cancellationToken);

            // Assert
            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentRepository>().Verify(c => c.GetAllCalculatedInvestmentsAsync(_getQuery.RescueDate), Times.Once);
            Assert.Empty(result.Investments);
        }
    }
}
