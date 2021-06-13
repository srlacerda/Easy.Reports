using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Entities;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;

namespace Easy.Reports.Application.Tests.UseCases.ConsolidatedReport
{
    [CollectionDefinition(nameof(GetHandlerCollection))]

    public class GetHandlerCollection : ICollectionFixture<GetHandlerTestsFixture> { }
    public class GetHandlerTestsFixture : IDisposable
    {
        public GetHandler GetHandler;
        public AutoMocker Mocker;

        public GetHandler CreateInstanceGetHandler()
        {
            Mocker = new AutoMocker();
            GetHandler = Mocker.CreateInstance<GetHandler>();
            return GetHandler;
        }

        public GetQuery GenerateGetQuery()
        {
            return new GetQuery(new DateTime(2021, 06, 14));
        }

        public List<Investment> GenerateInvestmentsNotOk()
        {
            return new List<Investment>();
        }

        public List<Investment> GenerateInvestmentsOk()
        {
            var investments = new List<Investment>
            {
                GenerateTreasuryDirect()
            };
            return investments;
        }

        private TreasuryDirect GenerateTreasuryDirect()
        {
            return new TreasuryDirect(
                investedValue: 1000,
                totalValue: 2000,
                dueDate: new DateTime(2015, 03, 01),
                purchaseDate: new DateTime(2015, 03, 01),
                name: "Tesouro Selic 2025");
        }
        public void Dispose()
        {
        }
    }
}
