using Easy.Reports.Domain.Interfaces;
using Easy.Reports.Infra.Data.Repositories;
using Moq;
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
        public FixedIncomeRepositoryTests(FixedIncomeRepositoryTestsFixture fixedIncomeRepositoryTestsFixture)
        {
            _fixedIncomeRepositoryTestsFixture = fixedIncomeRepositoryTestsFixture;
            _fixedIncomeRepository = _fixedIncomeRepositoryTestsFixture.CreateFixedIncomeRepository();
        }

        [Fact(DisplayName = "Get Investment Fund Ok")]
        [Trait("Category", "Repository - FixedIncome")]
        public async Task FixedIncomeRepository_FixedIncomeAsync_MustGetOk()
        {
            // Arrange
            var apiResponsefixedIncomeMockModel = _fixedIncomeRepositoryTestsFixture.GenerateApiResponseFixedIncomeMockModelOk();
            var fixedIncomeMockModelListFirst = apiResponsefixedIncomeMockModel.Content.FixedIncomeMockList.ToList().FirstOrDefault();

            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(apiResponsefixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeRepository.GetFixedIncomeAsync();
            var resultFixedIncomeFirst = result.FirstOrDefault();

            // Assert
            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Equal(fixedIncomeMockModelListFirst.InvestedCapital, resultFixedIncomeFirst.InvestedValue);
            Assert.Equal(fixedIncomeMockModelListFirst.CurrentCapital, resultFixedIncomeFirst.TotalValue);
            Assert.Equal(fixedIncomeMockModelListFirst.DueDate, resultFixedIncomeFirst.DueDate);
            Assert.Equal(fixedIncomeMockModelListFirst.OperationDate, resultFixedIncomeFirst.PurchaseDate);
            Assert.Equal(fixedIncomeMockModelListFirst.Name, resultFixedIncomeFirst.Name);
        }

        [Fact(DisplayName = "Get Investment Fund Not Ok")]
        [Trait("Category", "Repository - FixedIncome")]
        public async Task FixedIncomeRepository_FixedIncomeAsync_MustGetNotOK()
        {
            // Arrange
            var apiResponsefixedIncomeMockModel = _fixedIncomeRepositoryTestsFixture.GenerateApiResponseFixedIncomeMockModelNotOk();

            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetFixedIncomeAsync())
                .ReturnsAsync(apiResponsefixedIncomeMockModel);

            // Act
            var result = await _fixedIncomeRepository.GetFixedIncomeAsync();

            // Assert
            _fixedIncomeRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetFixedIncomeAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}