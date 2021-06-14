using Easy.Reports.Data.Repositories;
using Easy.Reports.Domain.Models;
using Moq.AutoMock;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [CollectionDefinition(nameof(TreasuryDirectRepositoryCollection))]
    public class TreasuryDirectRepositoryCollection : ICollectionFixture<TreasuryDirectRepositoryTestsFixture> { }
    public class TreasuryDirectRepositoryTestsFixture : IDisposable
    {
        public TreasuryDirectRepository TreasuryDirectRepository;
        public AutoMocker Mocker;

        public TreasuryDirectRepository CreateTreasuryDirectRepository()
        {
            Mocker = new AutoMocker();
            TreasuryDirectRepository = Mocker.CreateInstance<TreasuryDirectRepository>();
            return TreasuryDirectRepository;
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
                DueDate = new DateTime(2025, 03, 01),
                PurchaseDate = new DateTime(2015, 03, 01),
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
