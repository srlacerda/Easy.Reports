using Easy.Reports.Infra.Data.Repositories;
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

        [Fact(DisplayName = "Get Investment Fund Ok")]
        [Trait("Category", "Repository - InvestmentFund")]
        public async Task InvestmentFundRepository_InvestmentFundAsync_MustGetOk()
        {
            // Arrange
            var apiResponseinvestmentFundMockModel = _investmentFundRepositoryTestsFixture.GenerateApiResponseInvestmentFundMockModelOk();
            var investmentFundMockModelListFirst = apiResponseinvestmentFundMockModel.Content.InvestmentFundMockList.ToList().FirstOrDefault();

            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(apiResponseinvestmentFundMockModel);

            // Act
            var result = await _investmentFundRepository.GetInvestmentFundAsync(_rescueDate);
            var resultInvestmentFundFirst = result.FirstOrDefault();

            // Assert
            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Equal(investmentFundMockModelListFirst.InvestedCapital, resultInvestmentFundFirst.InvestedValue);
            Assert.Equal(investmentFundMockModelListFirst.CurrentValue, resultInvestmentFundFirst.TotalValue);
            Assert.Equal(investmentFundMockModelListFirst.RescueDate, resultInvestmentFundFirst.DueDate);
            Assert.Equal(investmentFundMockModelListFirst.PurchaseDate, resultInvestmentFundFirst.PurchaseDate);
            Assert.Equal(investmentFundMockModelListFirst.Name, resultInvestmentFundFirst.Name);
        }

        [Fact(DisplayName = "Get Investment Fund Not Ok")]
        [Trait("Category", "Repository - InvestmentFund")]
        public async Task InvestmentFundRepository_InvestmentFundAsync_MustGetNotOK()
        {
            // Arrange
            var apiResponseinvestmentFundMockModel = _investmentFundRepositoryTestsFixture.GenerateApiResponseInvestmentFundMockModelNotOk();

            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetInvestmentFundAsync())
                .ReturnsAsync(apiResponseinvestmentFundMockModel);

            // Act
            var result = await _investmentFundRepository.GetInvestmentFundAsync(_rescueDate);

            // Assert
            _investmentFundRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetInvestmentFundAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}