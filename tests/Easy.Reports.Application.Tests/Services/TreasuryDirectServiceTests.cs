using Easy.Reports.Application.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(TreasuryDirectServiceCollection))]
    public class TreasuryDirectServiceTests
    {
        private readonly TreasuryDirectServiceTestsFixture _treasuryDirectServiceTestsFixture;
        private readonly TreasuryDirectService _treasuryDirectService;
        private readonly DateTime _rescueDate;
        public TreasuryDirectServiceTests(TreasuryDirectServiceTestsFixture treasuryDirectServiceTestsFixture)
        {
            _treasuryDirectServiceTestsFixture = treasuryDirectServiceTestsFixture;
            _treasuryDirectService = _treasuryDirectServiceTestsFixture.CreateTreasuryDirectService();
            _rescueDate = new DateTime(2021, 06, 14);
        }

        [Fact(DisplayName = "Get Calculated Treasury Direct Ok")]
        [Trait("Category", "TreasuryDirect - Service")]
        public async Task TreasuryDirectService_CalculatedTreasuryDirectAsync_MustGetOk()
        {
            // Arrange
            var treasuryDirectMockModel = _treasuryDirectServiceTestsFixture.GenerateApiResponseTreasuryDirectMockModelOk();
            var treasuryDirectMockModelListFirst = treasuryDirectMockModel.Content.TreasuryDirectList.ToList().FirstOrDefault();

            _treasuryDirectServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetTreasuryDirectAsync())
                .ReturnsAsync(treasuryDirectMockModel);

            // Act
            var result = await _treasuryDirectService.GetCalculatedTreasuryDirectAsync(_rescueDate);
            var resultTreasuryDirectFirst = result.FirstOrDefault();

            // Assert
            _treasuryDirectServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetTreasuryDirectAsync(), Times.Once);
            Assert.Equal(treasuryDirectMockModelListFirst.InvestedValue, resultTreasuryDirectFirst.InvestedValue);
            Assert.Equal(treasuryDirectMockModelListFirst.TotalValue, resultTreasuryDirectFirst.TotalValue);
            Assert.Equal(treasuryDirectMockModelListFirst.DueDate, resultTreasuryDirectFirst.DueDate);
            Assert.Equal(treasuryDirectMockModelListFirst.PurchaseDate, resultTreasuryDirectFirst.PurchaseDate);
            Assert.Equal(treasuryDirectMockModelListFirst.Name, resultTreasuryDirectFirst.Name);
        }

        [Fact(DisplayName = "Get Calculated Treasury Direct Not Ok")]
        [Trait("Category", "TreasuryDirect - Service")]
        public async Task TreasuryDirectService_CalculatedTreasuryDirectAsync_MustGetNotOK()
        {
            // Arrange
            var treasuryDirectMockModel = _treasuryDirectServiceTestsFixture.GenerateApiResponseTreasuryDirectMockModelNotOk();

            _treasuryDirectServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetTreasuryDirectAsync())
                .ReturnsAsync(treasuryDirectMockModel);

            // Act
            var result = await _treasuryDirectService.GetCalculatedTreasuryDirectAsync(_rescueDate);

            // Assert
            _treasuryDirectServiceTestsFixture.Mocker.GetMock<IMockService>().Verify(m => m.GetTreasuryDirectAsync(), Times.Once);
            Assert.Empty(result);
        }
    }
}
