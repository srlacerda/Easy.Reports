using Easy.Reports.Data.Repositories;
using Easy.Reports.Domain.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [Collection(nameof(FixedIncomeRepositoryCollection))]
    public class FixedIncomeRepositoryTests
    {
        private readonly FixedIncomeRepositoryTestsFixture _fixedIncomeRepositoryTestsFixture;
        private readonly FixedIncomeRepository _fixedIncomeRepository;
        private readonly DateTime _rescueDate;
        public FixedIncomeRepositoryTests(FixedIncomeRepositoryTestsFixture fixedIncomeRepositoryTestsFixture)
        {
            _fixedIncomeRepositoryTestsFixture = fixedIncomeRepositoryTestsFixture;
            _fixedIncomeRepository = _fixedIncomeRepositoryTestsFixture.CreateFixedIncomeRepository();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Ok")]
        [Trait("Category", "Repository - FixedIncome")]
        public async Task FixedIncomeRepository_CalculatedFixedIncomeAsync_MustGetOk()
        {
            // Arrange
            var fixedIncomeMockModel = _fixedIncomeRepositoryTestsFixture.GenerateApiResponseFixedIncomeMockModelOk();
            var fixedIncomeMockModelListFirst = fixedIncomeMockModel.Content.FixedIncomeList.ToList().FirstOrDefault();

            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeRepository.GetCalculatedFixedIncomeAsync(_rescueDate);
            var resultFixedIncomeFirst = result.FirstOrDefault();

            // Assert
            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Equal(fixedIncomeMockModelListFirst.InvestedCapital, resultFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeMockModelListFirst.CurrentCapital, resultFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeMockModelListFirst.DueDate, resultFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeMockModelListFirst.OperationDate, resultFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeMockModelListFirst.Name, resultFixedIncomeFirst.Name);
        }

        [Fact(DisplayName = "Get Calculated Investment Fund Not Ok")]
        [Trait("Category", "Repository - FixedIncome")]
        public async Task FixedIncomeRepository_CalculatedFixedIncomeAsync_MustGetNotOK()
        {
            // Arrange
            var fixedIncomeMockModel = _fixedIncomeRepositoryTestsFixture.GenerateApiResponseFixedIncomeMockModelNotOk();

            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(fixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeRepository.GetCalculatedFixedIncomeAsync(_rescueDate);

            // Assert
            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}
