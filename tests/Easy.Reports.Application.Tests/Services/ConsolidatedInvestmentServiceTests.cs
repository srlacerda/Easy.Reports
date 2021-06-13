using Easy.Reports.Application.Services;
using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(ConsolidatedInvestmentServiceCollection))]
    public class ConsolidatedInvestmentServiceTests
    {
        private readonly ConsolidatedInvestmentServiceTestsFixture _consolidatedInvestmentServiceTestsFixture;
        private readonly ConsolidatedInvestmentService _consolidatedInvestmentService;
        private readonly DateTime _rescueDate;
        public ConsolidatedInvestmentServiceTests(ConsolidatedInvestmentServiceTestsFixture consolidatedInvestmentServiceTestsFixture)
        {
            _consolidatedInvestmentServiceTestsFixture = consolidatedInvestmentServiceTestsFixture;
            _consolidatedInvestmentService = _consolidatedInvestmentServiceTestsFixture.CreateConsolidatedInvestmentService();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Consolidated Investments Ok")]
        [Trait("Category", "ConsolidatedInvestment - Service")]
        public async Task ConsolidatedInvestmentService_CalculatedConsolidatedInvestmentAsync_MustGetOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentServiceTestsFixture.GenerateTresuryDirectListOk();
            var treasuryDirectFirst = treasuryDirectList.FirstOrDefault();

            var fixedIncomeList = _consolidatedInvestmentServiceTestsFixture.GenerateFixedIncomeListOk();
            var fixedIncomeFirst = fixedIncomeList.FirstOrDefault();

            var investmentFundList = _consolidatedInvestmentServiceTestsFixture.GenerateInvestmentFundListOk();
            var investmentFundFirst = investmentFundList.FirstOrDefault();

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectService>()
                .Setup(td => td.GetCalculatedTreasuryDirectAsync(_rescueDate))
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeService>()
                .Setup(fi => fi.GetCalculatedFixedIncomeAsync(_rescueDate))
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundService>()
                .Setup(i => i.GetCalculatedInvestmentFundAsync(_rescueDate))
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentService.GetAllCalculatedInvestmentsAsync(_rescueDate);
            var resultInvestmentsTreasuryDirectFirst = resultInvestments.OfType<TreasuryDirect>().FirstOrDefault();
            var resultInvestmentsFixedIncomeFirst = resultInvestments.OfType<FixedIncome>().FirstOrDefault();
            var resultInvestmentsInvestmentFundFirst = resultInvestments.OfType<InvestmentFund>().FirstOrDefault();

            // Assert
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectService>().Verify(td => td.GetCalculatedTreasuryDirectAsync(_rescueDate), Times.Once);
            Assert.Equal(treasuryDirectFirst.InvestedValue, resultInvestmentsTreasuryDirectFirst.InvestedValue);
            Assert.Equal(treasuryDirectFirst.TotalValue, resultInvestmentsTreasuryDirectFirst.TotalValue);
            Assert.Equal(treasuryDirectFirst.DueDate, resultInvestmentsTreasuryDirectFirst.DueDate);
            Assert.Equal(treasuryDirectFirst.PurchaseDate, resultInvestmentsTreasuryDirectFirst.PurchaseDate);
            Assert.Equal(treasuryDirectFirst.Name, resultInvestmentsTreasuryDirectFirst.Name);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeService>().Verify(fi => fi.GetCalculatedFixedIncomeAsync(_rescueDate), Times.Once);
            Assert.Equal(fixedIncomeFirst.InvestedValue, resultInvestmentsFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeFirst.TotalValue, resultInvestmentsFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeFirst.DueDate, resultInvestmentsFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeFirst.PurchaseDate, resultInvestmentsFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeFirst.Name, resultInvestmentsFixedIncomeFirst.Name);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundService>().Verify(i => i.GetCalculatedInvestmentFundAsync(_rescueDate), Times.Once);
            Assert.Equal(investmentFundFirst.InvestedValue, resultInvestmentsInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundFirst.TotalValue, resultInvestmentsInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundFirst.DueDate, resultInvestmentsInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundFirst.PurchaseDate, resultInvestmentsInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundFirst.Name, resultInvestmentsInvestmentFundFirst.Name);
        }

        [Fact(DisplayName = "Get Consolidated Investments Not Ok")]
        [Trait("Category", "ConsolidatedInvestment - Service")]
        public async Task ConsolidatedInvestmentService_CalculatedConsolidatedInvestmentAsync_MustGetNotOk()
        {
            // Arrange
            var treasuryDirectList = _consolidatedInvestmentServiceTestsFixture.GenerateTresuryDirectListNotOk();
            var fixedIncomeList = _consolidatedInvestmentServiceTestsFixture.GenerateFixedIncomeListNotOk();
            var investmentFundList = _consolidatedInvestmentServiceTestsFixture.GenerateInvestmentFundListNotOk();

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectService>()
                .Setup(td => td.GetCalculatedTreasuryDirectAsync(_rescueDate))
                .ReturnsAsync(treasuryDirectList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeService>()
                .Setup(fi => fi.GetCalculatedFixedIncomeAsync(_rescueDate))
                .ReturnsAsync(fixedIncomeList);

            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundService>()
                .Setup(i => i.GetCalculatedInvestmentFundAsync(_rescueDate))
                .ReturnsAsync(investmentFundList);

            // Act
            var resultInvestments = await _consolidatedInvestmentService.GetAllCalculatedInvestmentsAsync(_rescueDate);

            // Assert
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<ITreasuryDirectService>().Verify(td => td.GetCalculatedTreasuryDirectAsync(_rescueDate), Times.Once);
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IFixedIncomeService>().Verify(fi => fi.GetCalculatedFixedIncomeAsync(_rescueDate), Times.Once);
            _consolidatedInvestmentServiceTestsFixture.Mocker.GetMock<IInvestmentFundService>().Verify(i => i.GetCalculatedInvestmentFundAsync(_rescueDate), Times.Once);
            Assert.Empty(resultInvestments);
        }
    }
}
