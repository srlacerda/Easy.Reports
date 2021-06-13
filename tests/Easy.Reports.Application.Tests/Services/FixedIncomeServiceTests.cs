using Easy.Reports.Application.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(FixedIncomeServiceCollection))]
    public class FixedIncomeServiceTests
    {
        private readonly FixedIncomeServiceTestsFixture _fixedIncomeServiceTestsFixture;
        private readonly FixedIncomeService _fixedIncomeService;
        private readonly DateTime _rescueDate;
        public FixedIncomeServiceTests(FixedIncomeServiceTestsFixture fixedIncomeServiceTestsFixture)
        {
            _fixedIncomeServiceTestsFixture = fixedIncomeServiceTestsFixture;
            _fixedIncomeService = _fixedIncomeServiceTestsFixture.CreateFixedIncomeService();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Ok")]
        [Trait("Category", "FixedIncome - Service")]
        public async Task FixedIncomeService_CalculatedFixedIncomeAsync_MustGetOk()
        {
            // Arrange
            var fixedIncomeMockModel = _fixedIncomeServiceTestsFixture.GenerateApiResponseFixedIncomeMockModelOk();
            var fixedIncomeMockModelListFirst = fixedIncomeMockModel.Content.FixedIncomeList.ToList().FirstOrDefault();

            _fixedIncomeServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeService.GetCalculatedFixedIncomeAsync(_rescueDate);
            var resultFixedIncomeFirst = result.FirstOrDefault();

            // Assert
            _fixedIncomeServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Equal(fixedIncomeMockModelListFirst.InvestedCapital, resultFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeMockModelListFirst.CurrentCapital, resultFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeMockModelListFirst.DueDate, resultFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeMockModelListFirst.OperationDate, resultFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeMockModelListFirst.Name, resultFixedIncomeFirst.Name);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Not Ok")]
        [Trait("Category", "FixedIncome - Service")]
        public async Task FixedIncomeService_CalculatedFixedIncomeAsync_MustGetNotOK()
        {
            // Arrange
            var fixedIncomeMockModel = _fixedIncomeServiceTestsFixture.GenerateApiResponseFixedIncomeMockModelNotOk();

            _fixedIncomeServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeService.GetCalculatedFixedIncomeAsync(_rescueDate);

            // Assert
            _fixedIncomeServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}
