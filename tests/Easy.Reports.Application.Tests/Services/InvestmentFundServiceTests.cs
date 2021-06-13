using Easy.Reports.Application.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(InvestmentFundServiceCollection))]
    public class InvestmentFundServiceTests 
    {
        private readonly InvestmentFundServiceTestsFixture _investmentFundServiceTestsFixture;
        private readonly InvestmentFundService _investmentFundService;
        private readonly DateTime _rescueDate;
        public InvestmentFundServiceTests(InvestmentFundServiceTestsFixture investmentFundServiceTestsFixture)
        {
            _investmentFundServiceTestsFixture = investmentFundServiceTestsFixture;
            _investmentFundService = _investmentFundServiceTestsFixture.CreateInvestmentFundService();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Ok")]
        [Trait("Category", "InvestmentFund - Service")]
        public async Task InvestmentFundService_CalculatedInvestmentFundAsync_MustGetOk()
        {
            // Arrange
            var investmentFundMockModel = _investmentFundServiceTestsFixture.GenerateApiResponseInvestmentFundMockModelOk();
            var investmentFundMockModelListFirst = investmentFundMockModel.Content.InvestmentFundList.ToList().FirstOrDefault();

            _investmentFundServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundMockModel);

            // Act
            var result = await _investmentFundService.GetCalculatedInvestmentFundAsync(_rescueDate);
            var resultInvestmentFundFirst = result.FirstOrDefault();

            // Assert
            _investmentFundServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Equal(investmentFundMockModelListFirst.InvestedCapital, resultInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundMockModelListFirst.CurrentValue, resultInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundMockModelListFirst.RescueDate, resultInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundMockModelListFirst.PurchaseDate, resultInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundMockModelListFirst.Name, resultInvestmentFundFirst.Name);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Not Ok")]
        [Trait("Category", "InvestmentFund - Service")]
        public async Task InvestmentFundService_CalculatedInvestmentFundAsync_MustGetNotOK()
        {
            // Arrange
            var investmentFundMockModel = _investmentFundServiceTestsFixture.GenerateApiResponseInvestmentFundMockModelNotOk();

            _investmentFundServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundMockModel);

            // Act
            var result = await _investmentFundService.GetCalculatedInvestmentFundAsync(_rescueDate);

            // Assert
            _investmentFundServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}
