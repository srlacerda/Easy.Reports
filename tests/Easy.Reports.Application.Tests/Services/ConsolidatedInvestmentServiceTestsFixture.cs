using Easy.Reports.Domain.Entities;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;
using Easy.Reports.Domain.Models;
using Easy.Reports.Application.Services;

namespace Easy.Reports.Application.Tests.Services
{
    [CollectionDefinition(nameof(ConsolidatedInvestmentServiceCollection))]
    public class ConsolidatedInvestmentServiceCollection : ICollectionFixture<ConsolidatedInvestmentServiceTestsFixture> { }
    public class ConsolidatedInvestmentServiceTestsFixture : IDisposable
    {
        public ConsolidatedInvestmentService ConsolidatedInvestmentService;
        public AutoMocker Mocker;

        public ConsolidatedInvestmentService CreateConsolidatedInvestmentService()
        {
            Mocker = new AutoMocker();
            ConsolidatedInvestmentService = Mocker.CreateInstance<ConsolidatedInvestmentService>();
            return ConsolidatedInvestmentService;
        }

        public IEnumerable<TreasuryDirect> GenerateTresuryDirectListOk()
        {
            return new List<TreasuryDirect>
            {
                new TreasuryDirect(new TreasuryDirectMock
                {
                    InvestedValue = 799.4720m,
                    TotalValue = 829.68m,
                    DueDate = new DateTime(2025, 03, 01),
                    PurchaseDate = new DateTime(2015, 03, 01),
                    Name = "Tesouro Selic 2025"
                })
            };
        }

        public IEnumerable<TreasuryDirect> GenerateTresuryDirectListNotOk()
        {
            return new List<TreasuryDirect>();
        }

        public IEnumerable<FixedIncome> GenerateFixedIncomeListOk()
        {
            return new List<FixedIncome>
            {
                new FixedIncome(new FixedIncomeMock
                {
                    InvestedCapital = 2000.0m,
                    CurrentCapital = 2097.85m,
                    DueDate= new DateTime(2021, 03, 09),
                    OperationDate = new DateTime(2019, 03, 14),
                    Name= "BANCO MAXIMA"
                })
            };
        }
        public IEnumerable<FixedIncome> GenerateFixedIncomeListNotOk()
        {
            return new List<FixedIncome>();
        }

        public IEnumerable<InvestmentFund> GenerateInvestmentFundListOk()
        {
            return new List<InvestmentFund>
            {
                new InvestmentFund(new InvestmentFundMock
                {
                    InvestedCapital = 1000.0m,
                    CurrentValue = 1159m,
                    RescueDate = new DateTime(2022, 10, 01),
                    PurchaseDate = new DateTime(2017, 10, 01),
                    Name = "ALASKA"
                })
            };
        }
        public IEnumerable<InvestmentFund> GenerateInvestmentFundListNotOk()
        {
            return new List<InvestmentFund>();
        }

        public void Dispose() { }
    }
}
