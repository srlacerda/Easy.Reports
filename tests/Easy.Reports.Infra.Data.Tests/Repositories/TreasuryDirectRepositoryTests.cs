using Easy.Reports.Infra.Data.Repositories;
using Easy.Reports.Domain.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [Collection(nameof(TreasuryDirectRepositoryCollection))]
    public class TreasuryDirectRepositoryTests
    {
        private readonly TreasuryDirectRepositoryTestsFixture _treasuryDirectRepositoryTestsFixture;
        private readonly TreasuryDirectRepository _treasuryDirectRepository;
        private readonly DateTime _rescueDate;
        public TreasuryDirectRepositoryTests(TreasuryDirectRepositoryTestsFixture treasuryDirectRepositoryTestsFixture)
        {
            _treasuryDirectRepositoryTestsFixture = treasuryDirectRepositoryTestsFixture;
            _treasuryDirectRepository = _treasuryDirectRepositoryTestsFixture.CreateTreasuryDirectRepository();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Treasury Direct Ok")]
        [Trait("Category", "Repository - TreasuryDirect")]
        public async Task TreasuryDirectRepository_TreasuryDirectAsync_MustGetOk()
        {
            // Arrange
            var apiResponseTreasuryDirectMockModel = _treasuryDirectRepositoryTestsFixture.GenerateApiResponseTreasuryDirectMockModelOk();
            var treasuryDirectMockModelListFirst = apiResponseTreasuryDirectMockModel.Content.TreasuryDirectMockList.ToList().FirstOrDefault();

            _treasuryDirectRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetTreasuryDirectAsync())
                .ReturnsAsync(apiResponseTreasuryDirectMockModel);

            // Act
            var result = await _treasuryDirectRepository.GetTreasuryDirectAsync(_rescueDate);
            var resultTreasuryDirectFirst = result.FirstOrDefault();

            // Assert
            _treasuryDirectRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetTreasuryDirectAsync(), Times.Once);
            Assert.Equal(treasuryDirectMockModelListFirst.InvestedValue, resultTreasuryDirectFirst.InvestedValue);
            Assert.Equal(treasuryDirectMockModelListFirst.TotalValue, resultTreasuryDirectFirst.TotalValue);
            Assert.Equal(treasuryDirectMockModelListFirst.DueDate, resultTreasuryDirectFirst.DueDate);
            Assert.Equal(treasuryDirectMockModelListFirst.PurchaseDate, resultTreasuryDirectFirst.PurchaseDate);
            Assert.Equal(treasuryDirectMockModelListFirst.Name, resultTreasuryDirectFirst.Name);
        }

        [Fact(DisplayName = "Get Treasury Direct Not Ok")]
        [Trait("Category", "Repository - TreasuryDirect")]
        public async Task TreasuryDirectRepository_TreasuryDirectAsync_MustGetNotOK()
        {
            // Arrange
            var apiResponseTreasuryDirectMockModel = _treasuryDirectRepositoryTestsFixture.GenerateApiResponseTreasuryDirectMockModelNotOk();

            _treasuryDirectRepositoryTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetTreasuryDirectAsync())
                .ReturnsAsync(apiResponseTreasuryDirectMockModel);

            // Act
            var result = await _treasuryDirectRepository.GetTreasuryDirectAsync(_rescueDate);

            // Assert
            _treasuryDirectRepositoryTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetTreasuryDirectAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}