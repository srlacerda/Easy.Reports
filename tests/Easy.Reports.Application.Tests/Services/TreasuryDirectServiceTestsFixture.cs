using Easy.Reports.Application.Services;
using Easy.Reports.Domain.Models;
using Moq.AutoMock;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Easy.Reports.Application.Tests.Services
{
    [CollectionDefinition(nameof(TreasuryDirectServiceCollection))]
    public class TreasuryDirectServiceCollection : ICollectionFixture<TreasuryDirectServiceTestsFixture> { }
    public class TreasuryDirectServiceTestsFixture : IDisposable
    {
        public TreasuryDirectService TreasuryDirectService;
        public AutoMocker Mocker;

        public TreasuryDirectService CreateTreasuryDirectService()
        {
            Mocker = new AutoMocker();
            TreasuryDirectService = Mocker.CreateInstance<TreasuryDirectService>();
            return TreasuryDirectService;
        }

        public ApiResponse<TreasuryDirectMockModel> GenerateApiResponseTreasuryDirectMockModelOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            var treasuryDirectMockModel = new TreasuryDirectMockModel();
            var treasuryDirectMockList = new List<TreasuryDirectMock>
            {
                GenerateTreasuryDirectMock()
            };
            
            treasuryDirectMockModel.TreasuryDirectList = treasuryDirectMockList;
            return new ApiResponse<TreasuryDirectMockModel>(httpResponseMessage, treasuryDirectMockModel);
        }

        public ApiResponse<TreasuryDirectMockModel> GenerateApiResponseTreasuryDirectMockModelNotOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            return new ApiResponse<TreasuryDirectMockModel>(httpResponseMessage, null);
        }

        private TreasuryDirectMock GenerateTreasuryDirectMock()
        {
            return new TreasuryDirectMock
            {
                InvestedValue = 799.4720m,
                TotalValue = 829.68m,
                DueDate = new DateTime(2025,03,01),
                PurchaseDate = new DateTime(2015,03,01),
                Iof = 0,
                Index = "SELIC",
                InvestmentType = "TD",
                Name = "Tesouro Selic 2025"
            };
        }

        public void Dispose()
        {
        }
    }
}
