using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Services;
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

        public GetHandlerTests(GetHandlerTestsFixture getHandlerTestsFixture)
        {
            _getHandlerTestsFixture = getHandlerTestsFixture;
            _getHandler = _getHandlerTestsFixture.CreateInstanceGetHandler();
        }

        [Fact(DisplayName = "Get Consolidate Report Sucess")]
        [Trait("Categoria", "ConsolidateReport - GetHandler")]
        public async Task ConsolidateReport_Get_SucessAsync()
        {
            // Arrange
            CancellationToken cancellationToken = CancellationToken.None;
            var getQuery = _getHandlerTestsFixture.GenerateGetQuery();
            var investments = _getHandlerTestsFixture.GenerateInvestments();

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

            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentService>()
                .Setup(c => c.GetAllCalculatedInvestmentsAsync(getQuery.RescueDate))
                .ReturnsAsync(investments);

            // Act
            var result = (await _getHandler.Handle(getQuery, cancellationToken)).Investments.ToList();

            // Assert
            _getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentService>().Verify(c => c.GetAllCalculatedInvestmentsAsync(getQuery.RescueDate), Times.Once);
            Assert.Equal(investments.FirstOrDefault().InvestedValue, result.FirstOrDefault().InvestedValue);
            Assert.Equal(investments.FirstOrDefault().TotalValue, result.FirstOrDefault().TotalValue);
            Assert.Equal(investments.FirstOrDefault().DueDate, result.FirstOrDefault().DueDate);
            Assert.Equal(investments.FirstOrDefault().PurchaseDate, result.FirstOrDefault().PurchaseDate);
            Assert.Equal(investments.FirstOrDefault().Name, result.FirstOrDefault().Name);
        }
    }
}
