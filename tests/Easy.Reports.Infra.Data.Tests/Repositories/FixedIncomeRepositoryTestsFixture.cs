using Easy.Reports.Infra.Data.Repositories;
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
    [CollectionDefinition(nameof(FixedIncomeRepositoryCollection))]
    public class FixedIncomeRepositoryCollection : ICollectionFixture<FixedIncomeRepositoryTestsFixture> { }
    public class FixedIncomeRepositoryTestsFixture : IDisposable
    {
        public FixedIncomeRepository FixedIncomeRepository;
        public AutoMocker Mocker;

        public FixedIncomeRepository CreateFixedIncomeRepository()
        {
            Mocker = new AutoMocker();
            FixedIncomeRepository = Mocker.CreateInstance<FixedIncomeRepository>();
            return FixedIncomeRepository;
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
                InvestedCapital = 2000.0m,
                CurrentCapital = 2097.85m,
                Quantitiy = 2.0m,
                DueDate = new DateTime(2021, 03, 09),
                Iof = 0,
                OtherFees = 0,
                Fees = 0,
                Index = "97% do CDI",
                InvestmentType = "LCI",
                Name = "BANCO MAXIMA",
                GuaranteedFgc = true,
                OperationDate = new DateTime(2019, 03, 14),
                UnitPrice = 1048.92745m,
                Primary = false
            };
        }

        public void Dispose()
        {
        }
    }
}
