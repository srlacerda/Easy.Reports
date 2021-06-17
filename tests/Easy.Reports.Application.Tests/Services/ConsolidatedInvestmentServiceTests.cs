using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Easy.Reports.Application.Services;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(ConsolidatedInvestmentServiceCollection))]
    public class ConsolidatedInvestmentServiceTests
    {
        private readonly ConsolidatedInvestmentServiceTestsFixture _consolidatedInvestmentServiceTestsFixture;
        private readonly ConsolidatedInvestmentService _consolidatedInvestmentService;
        private readonly DateTime _rescueDate;
        public ConsolidatedInvestmentServiceTests(ConsolidatedInvestmentServiceTestsFixture consolidatedInvestmentRepositoryTestsFixture)
        {
            _consolidatedInvestmentServiceTestsFixture = consolidatedInvestmentRepositoryTestsFixture;
            _consolidatedInvestmentService = _consolidatedInvestmentServiceTestsFixture.CreateConsolidatedInvestmentService();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Consolidated Investments Ok")]
        [Trait("Category", "Service - ConsolidatedInvestment")]
        public async Task ConsolidatedInvestmentRepository_CalculatedConsolidatedInvestmentAsync_MustGetOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentServiceTestsFixture.GenerateTresuryDirectListOk();
            var treasuryDirectFirst = treasuryDirectList.FirstOrDefault();

            var fixedIncomeList = _consolidatedInvestmentServiceTestsFixture.GenerateFixedIncomeListOk();
            var fixedIncomeFirst = fixedIncomeList.FirstOrDefault();

            var investmentFundList = _consolidatedInvestmentServiceTestsFixture.GenerateInvestmentFundListOk();
            var investmentFundFirst = investmentFundList.FirstOrDefault();

            #region mocking IMemoryCache
            string keyPayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>()
                .Setup(td => td.GetTreasuryDirectAsync())
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeRepository>()
                .Setup(fi => fi.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundRepository>()
                .Setup(i => i.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentService.GetAllCalculatedInvestmentsAsync(_rescueDate);
            var resultInvestmentsTreasuryDirectFirst = resultInvestments.OfType<TreasuryDirect>().FirstOrDefault();
            var resultInvestmentsFixedIncomeFirst = resultInvestments.OfType<FixedIncome>().FirstOrDefault();
            var resultInvestmentsInvestmentFundFirst = resultInvestments.OfType<InvestmentFund>().FirstOrDefault();

            // Assert
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>().Verify(td => td.GetTreasuryDirectAsync(), Times.Once);
            Assert.Equal(treasuryDirectFirst.InvestedValue, resultInvestmentsTreasuryDirectFirst.InvestedValue);
            Assert.Equal(treasuryDirectFirst.TotalValue, resultInvestmentsTreasuryDirectFirst.TotalValue);
            Assert.Equal(treasuryDirectFirst.DueDate, resultInvestmentsTreasuryDirectFirst.DueDate);
            Assert.Equal(treasuryDirectFirst.PurchaseDate, resultInvestmentsTreasuryDirectFirst.PurchaseDate);
            Assert.Equal(treasuryDirectFirst.Name, resultInvestmentsTreasuryDirectFirst.Name);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeRepository>().Verify(fi => fi.GetFixedIncomeAsync(), Times.Once);
            Assert.Equal(fixedIncomeFirst.InvestedValue, resultInvestmentsFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeFirst.TotalValue, resultInvestmentsFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeFirst.DueDate, resultInvestmentsFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeFirst.PurchaseDate, resultInvestmentsFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeFirst.Name, resultInvestmentsFixedIncomeFirst.Name);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundRepository>().Verify(i => i.GetInvestmentFundAsync(), Times.Once);
            Assert.Equal(investmentFundFirst.InvestedValue, resultInvestmentsInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundFirst.TotalValue, resultInvestmentsInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundFirst.DueDate, resultInvestmentsInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundFirst.PurchaseDate, resultInvestmentsInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundFirst.Name, resultInvestmentsInvestmentFundFirst.Name);
        }

        [Fact(DisplayName = "Get Consolidated Investments Not Ok")]
        [Trait("Category", "Service - ConsolidatedInvestment")]
        public async Task ConsolidatedInvestmentRepository_CalculatedConsolidatedInvestmentAsync_MustGetNotOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentServiceTestsFixture.GenerateTresuryDirectListNotOk();
            var fixedIncomeList = _consolidatedInvestmentServiceTestsFixture.GenerateFixedIncomeListNotOk();
            var investmentFundList = _consolidatedInvestmentServiceTestsFixture.GenerateInvestmentFundListNotOk();

            #region mocking IMemoryCache
            string keyPayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>()
                .Setup(td => td.GetTreasuryDirectAsync())
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeRepository>()
                .Setup(fi => fi.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundRepository>()
                .Setup(i => i.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentService.GetAllCalculatedInvestmentsAsync(_rescueDate);

            // Assert
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>().Verify(td => td.GetTreasuryDirectAsync(), Times.Once);
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeRepository>().Verify(fi => fi.GetFixedIncomeAsync(), Times.Once);
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundRepository>().Verify(i => i.GetInvestmentFundAsync(), Times.Once);
            Assert.True(resultInvestments == null);
        }
    }
}
