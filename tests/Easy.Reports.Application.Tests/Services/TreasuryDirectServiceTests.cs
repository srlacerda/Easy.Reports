using Easy.Reports.Application.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [Collection(nameof(TreasuryDirectServiceCollection))]
    public class TreasuryDirectServiceTests
    {
        private readonly TreasuryDirectServiceTestsFixture _treasuryDirectServiceTestsFixture;
        private readonly TreasuryDirectService _treasuryDirectService;
        public TreasuryDirectServiceTests(TreasuryDirectServiceTestsFixture treasuryDirectServiceTestsFixture)
        {
            _treasuryDirectServiceTestsFixture = treasuryDirectServiceTestsFixture;
            _treasuryDirectService = _treasuryDirectServiceTestsFixture.CreateTreasuryDirectService();
        }

        [Fact(DisplayName = "Must Get Sucessfully")]
        [Trait("Category", "TreasuryDirect - Service")]
        public async Task TreasuryDirectService_GetCalculatedTreasuryDirectAsync_MustGetSucessfully()
        {
            // Arrange
            var rescueDate = new DateTime(2021, 06, 14);
            var treasuryDirectMockModel = _treasuryDirectServiceTestsFixture.GenerateApiResponseTreasuryDirectMockModelOk();

            _treasuryDirectServiceTestsFixture.Mocker.GetMock<IMockService>()
                .Setup(mc => mc.GetTreasuryDirectAsync())
                .ReturnsAsync(treasuryDirectMockModel);

            // Act
            var result = await _treasuryDirectService.GetCalculatedTreasuryDirectAsync(rescueDate);

            Assert.True(result.ToString() == "");
            // Assert
            //_getHandlerTestsFixture.Mocker.GetMock<IConsolidatedInvestmentService>().Verify(c => c.GetAllCalculatedInvestmentsAsync(getQuery.RescueDate), Times.Once);
            //Assert.Equal(investments.FirstOrDefault().InvestedValue, result.FirstOrDefault().InvestedValue);
            //Assert.Equal(investments.FirstOrDefault().TotalValue, result.FirstOrDefault().TotalValue);
            //Assert.Equal(investments.FirstOrDefault().DueDate, result.FirstOrDefault().DueDate);
            //Assert.Equal(investments.FirstOrDefault().PurchaseDate, result.FirstOrDefault().PurchaseDate);
            //Assert.Equal(investments.FirstOrDefault().Name, result.FirstOrDefault().Name);
        }
    }
}
