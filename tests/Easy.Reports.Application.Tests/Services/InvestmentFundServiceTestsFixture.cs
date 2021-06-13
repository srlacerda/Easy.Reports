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
    [CollectionDefinition(nameof(InvestmentFundServiceCollection))]
    public class InvestmentFundServiceCollection : ICollectionFixture<InvestmentFundServiceTestsFixture> { }
    public class InvestmentFundServiceTestsFixture : IDisposable
    {
        public InvestmentFundService InvestmentFundService;
        public AutoMocker Mocker;

        public InvestmentFundService CreateInvestmentFundService()
        {
            Mocker = new AutoMocker();
            InvestmentFundService = Mocker.CreateInstance<InvestmentFundService>();
            return InvestmentFundService;
        }

        public ApiResponse<InvestmentFundMockModel> GenerateApiResponseInvestmentFundMockModelOk()
        {
            var httpResponseMessage = new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
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
            var httpResponseMessage = new HttpResponseMessage {StatusCode = HttpStatusCode.InternalServerError};
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
