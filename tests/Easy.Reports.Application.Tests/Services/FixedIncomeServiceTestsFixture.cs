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
    [CollectionDefinition(nameof(FixedIncomeServiceCollection))]
    public class FixedIncomeServiceCollection : ICollectionFixture<FixedIncomeServiceTestsFixture> { }
    public class FixedIncomeServiceTestsFixture : IDisposable
    {
        public FixedIncomeService FixedIncomeService;
        public AutoMocker Mocker;

        public FixedIncomeService CreateFixedIncomeService()
        {
            Mocker = new AutoMocker();
            FixedIncomeService = Mocker.CreateInstance<FixedIncomeService>();
            return FixedIncomeService;
        }

        public ApiResponse<FixedIncomeMockModel> GenerateApiResponseFixedIncomeMockModelOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            var fixedIncomeMockModel = new FixedIncomeMockModel();
            var fixedIncomeMockList = new List<FixedIncomeMock>
            {
                GenerateFixedIncomeMock()
            };

            fixedIncomeMockModel.FixedIncomeList = fixedIncomeMockList;
            return new ApiResponse<FixedIncomeMockModel>(httpResponseMessage, fixedIncomeMockModel);
        }

        public ApiResponse<FixedIncomeMockModel> GenerateApiResponseFixedIncomeMockModelNotOk()
        {
            var httpResponseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            return new ApiResponse<FixedIncomeMockModel>(httpResponseMessage, null);
        }

        private FixedIncomeMock GenerateFixedIncomeMock()
        {
            return new FixedIncomeMock
            {
                InvestedCapital = 799.4720m,
                CurrentCapital = 829.68m,
                Quantitiy = 1,
                DueDate = new DateTime(2025, 03, 01),
                Iof = 0,
                OtherFees = 0,
                Fees = 0,
                Index = "SELIC",
                InvestmentType = "TD",
                Name = "Tesouro Selic 2025",
                GuaranteedFgc = false,
                OperationDate = new DateTime(2015, 03, 01),
                UnitPrice = 1m,
                Primary = false
            };
        }

        public void Dispose()
        {
        }
    }
}
