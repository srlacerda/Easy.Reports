using Easy.Reports.Data.Repositories;
using Easy.Reports.Domain.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [Collection(nameof(InvestmentFundRepositoryCollection))]
    public class InvestmentFundRepositoryTests
    {
        private readonly InvestmentFundRepositoryTestsFixture _investmentFundRepositoryTestsFixture;
        private readonly InvestmentFundRepository _investmentFundRepository;
        private readonly DateTime _rescueDate;
        public InvestmentFundRepositoryTests(InvestmentFundRepositoryTestsFixture investmentFundRepositoryTestsFixture)
        {
            _investmentFundRepositoryTestsFixture = investmentFundRepositoryTestsFixture;
            _investmentFundRepository = _investmentFundRepositoryTestsFixture.CreateInvestmentFundRepository();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Ok")]
        [Trait("Category", "Repository - InvestmentFund")]
        public async Task InvestmentFundRepository_CalculatedInvestmentFundAsync_MustGetOk()
        {
            // Arrange
            var investmentFundMockModel = _investmentFundRepositoryTestsFixture.GenerateApiResponseInvestmentFundMockModelOk();
            var investmentFundMockModelListFirst = investmentFundMockModel.Content.InvestmentFundList.ToList().FirstOrDefault();

            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundMockModel);

            // Act
            var result = await _investmentFundRepository.GetCalculatedInvestmentFundAsync(_rescueDate);
            var resultInvestmentFundFirst = result.FirstOrDefault();

            // Assert
            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Equal(investmentFundMockModelListFirst.InvestedCapital, resultInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundMockModelListFirst.CurrentValue, resultInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundMockModelListFirst.RescueDate, resultInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundMockModelListFirst.PurchaseDate, resultInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundMockModelListFirst.Name, resultInvestmentFundFirst.Name);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Not Ok")]
        [Trait("Category", "Repository - InvestmentFund")]
        public async Task InvestmentFundRepository_CalculatedInvestmentFundAsync_MustGetNotOK()
        {
            // Arrange
            var investmentFundMockModel = _investmentFundRepositoryTestsFixture.GenerateApiResponseInvestmentFundMockModelNotOk();

            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(investmentFundMockModel);

            // Act
            var result = await _investmentFundRepository.GetCalculatedInvestmentFundAsync(_rescueDate);

            // Assert
            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}
