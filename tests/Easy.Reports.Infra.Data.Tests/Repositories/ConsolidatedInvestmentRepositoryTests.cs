using Easy.Reports.Infra.Data.Repositories;
using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [Collection(nameof(ConsolidatedInvestmentRepositoryCollection))]
    public class ConsolidatedInvestmentRepositoryTests
    {
        private readonly ConsolidatedInvestmentRepositoryTestsFixture _consolidatedInvestmentRepositoryTestsFixture;
        private readonly ConsolidatedInvestmentRepository _consolidatedInvestmentRepository;
        private readonly DateTime _rescueDate;
        public ConsolidatedInvestmentRepositoryTests(ConsolidatedInvestmentRepositoryTestsFixture consolidatedInvestmentRepositoryTestsFixture)
        {
            _consolidatedInvestmentRepositoryTestsFixture = consolidatedInvestmentRepositoryTestsFixture;
            _consolidatedInvestmentRepository = _consolidatedInvestmentRepositoryTestsFixture.CreateConsolidatedInvestmentRepository();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Consolidated Investments Ok")]
        [Trait("Category", "Repository - ConsolidatedInvestment")]
        public async Task ConsolidatedInvestmentRepository_CalculatedConsolidatedInvestmentAsync_MustGetOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentRepositoryTestsFixture.GenerateTresuryDirectListOk();
            var treasuryDirectFirst = treasuryDirectList.FirstOrDefault();

            var fixedIncomeList = _consolidatedInvestmentRepositoryTestsFixture.GenerateFixedIncomeListOk();
            var fixedIncomeFirst = fixedIncomeList.FirstOrDefault();

            var investmentFundList = _consolidatedInvestmentRepositoryTestsFixture.GenerateInvestmentFundListOk();
            var investmentFundFirst = investmentFundList.FirstOrDefault();

            #region mocking IMemoryCache
            string keyPayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>()
                .Setup(td => td.GetTreasuryDirectAsync(_rescueDate))
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IFixedIncomeRepository>()
                .Setup(fi => fi.GetFixedIncomeAsync(_rescueDate))
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IInvestmentFundRepository>()
                .Setup(i => i.GetInvestmentFundAsync(_rescueDate))
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(_rescueDate);
            var resultInvestmentsTreasuryDirectFirst = resultInvestments.OfType<TreasuryDirect>().FirstOrDefault();
            var resultInvestmentsFixedIncomeFirst = resultInvestments.OfType<FixedIncome>().FirstOrDefault();
            var resultInvestmentsInvestmentFundFirst = resultInvestments.OfType<InvestmentFund>().FirstOrDefault();

            // Assert
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>().Verify(td => td.GetTreasuryDirectAsync(_rescueDate), Times.Once);
            Assert.Equal(treasuryDirectFirst.InvestedValue, resultInvestmentsTreasuryDirectFirst.InvestedValue);
            Assert.Equal(treasuryDirectFirst.TotalValue, resultInvestmentsTreasuryDirectFirst.TotalValue);
            Assert.Equal(treasuryDirectFirst.DueDate, resultInvestmentsTreasuryDirectFirst.DueDate);
            Assert.Equal(treasuryDirectFirst.PurchaseDate, resultInvestmentsTreasuryDirectFirst.PurchaseDate);
            Assert.Equal(treasuryDirectFirst.Name, resultInvestmentsTreasuryDirectFirst.Name);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IFixedIncomeRepository>().Verify(fi => fi.GetFixedIncomeAsync(_rescueDate), Times.Once);
            Assert.Equal(fixedIncomeFirst.InvestedValue, resultInvestmentsFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeFirst.TotalValue, resultInvestmentsFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeFirst.DueDate, resultInvestmentsFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeFirst.PurchaseDate, resultInvestmentsFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeFirst.Name, resultInvestmentsFixedIncomeFirst.Name);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IInvestmentFundRepository>().Verify(i => i.GetInvestmentFundAsync(_rescueDate), Times.Once);
            Assert.Equal(investmentFundFirst.InvestedValue, resultInvestmentsInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundFirst.TotalValue, resultInvestmentsInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundFirst.DueDate, resultInvestmentsInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundFirst.PurchaseDate, resultInvestmentsInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundFirst.Name, resultInvestmentsInvestmentFundFirst.Name);


        }

        [Fact(DisplayName = "Get Consolidated Investments Not Ok")]
        [Trait("Category", "Repository - ConsolidatedInvestment")]
        public async Task ConsolidatedInvestmentRepository_CalculatedConsolidatedInvestmentAsync_MustGetNotOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentRepositoryTestsFixture.GenerateTresuryDirectListNotOk();
            var fixedIncomeList = _consolidatedInvestmentRepositoryTestsFixture.GenerateFixedIncomeListNotOk();
            var investmentFundList = _consolidatedInvestmentRepositoryTestsFixture.GenerateInvestmentFundListNotOk();

            #region mocking IMemoryCache
            string keyPayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IMemoryCache>()
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(_consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>().Object);

            object valuePayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>()
               .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ICacheEntry>()
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);
            #endregion

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>()
                .Setup(td => td.GetTreasuryDirectAsync(_rescueDate))
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IFixedIncomeRepository>()
                .Setup(fi => fi.GetFixedIncomeAsync(_rescueDate))
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IInvestmentFundRepository>()
                .Setup(i => i.GetInvestmentFundAsync(_rescueDate))
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(_rescueDate);

            // Assert
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<ITreasuryDirectRepository>().Verify(td => td.GetTreasuryDirectAsync(_rescueDate), Times.Once);
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IFixedIncomeRepository>().Verify(fi => fi.GetFixedIncomeAsync(_rescueDate), Times.Once);
            _consolidatedInvestmentRepositoryTestsFixture.Mocker.GetMock<IInvestmentFundRepository>().Verify(i => i.GetInvestmentFundAsync(_rescueDate), Times.Once);
            Assert.Empty(resultInvestments);
        }
    }
}
