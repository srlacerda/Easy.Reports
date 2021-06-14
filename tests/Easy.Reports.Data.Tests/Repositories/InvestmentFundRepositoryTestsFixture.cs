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
    [CollectionDefinition(nameof(InvestmentFundRepositoryCollection))]
    public class InvestmentFundRepositoryCollection : ICollectionFixture<InvestmentFundRepositoryTestsFixture> { }
    public class InvestmentFundRepositoryTestsFixture : IDisposable
    {
        public InvestmentFundRepository InvestmentFundRepository;
        public AutoMocker Mocker;

        public InvestmentFundRepository CreateInvestmentFundRepository()
        {
            Mocker = new AutoMocker();
            InvestmentFundRepository = Mocker.CreateInstance<InvestmentFundRepository>();
            return InvestmentFundRepository;
        }

        public ApiResponse<InvestmentFundMockModel> GenerateApiResponseInvestmentFundMockModelOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            var investmentFundMockModel = new InvestmentFundMockModel();
            var investmentFundMockList = new List<InvestmentFundMock>
            {
                GenerateInvestmentFundMock()
            };

            investmentFundMockModel.InvestmentFundList = investmentFundMockList;
            return new ApiResponse<InvestmentFundMockModel>(httpResponseMessage, investmentFundMockModel);
        }

        public ApiResponse<InvestmentFundMockModel> GenerateApiResponseInvestmentFundMockModelNotOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            return new ApiResponse<InvestmentFundMockModel>(httpResponseMessage, null);
        }

        private InvestmentFundMock GenerateInvestmentFundMock()
        {
            return new InvestmentFundMock
            {
                InvestedCapital = 1000.0m,
                CurrentValue = 1159m,
                RescueDate = new DateTime(2022, 10, 01),
                PurchaseDate = new DateTime(2017, 10, 01),
                Iof = 0,
                Name = "ALASKA",
                TotalFees = 53.49m,
                Quantity = 1,
            };
        }

        public void Dispose()
        {
        }
    }
}
