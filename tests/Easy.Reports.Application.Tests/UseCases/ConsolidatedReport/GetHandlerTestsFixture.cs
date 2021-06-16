using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Models;
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
            return new TreasuryDirect(new TreasuryDirectMock
            {
                InvestedValue = 1000,
                TotalValue = 2000,
                DueDate = new DateTime(2015, 03, 01),
                PurchaseDate = new DateTime(2015, 03, 01),
                Name = "Tesouro Selic 2025"
            });
        }
        public void Dispose() { }
    }
}