using Easy.Reports.Data.Repositories;
using Easy.Reports.Domain.Entities;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;

namespace Easy.Reports.Data.Tests.Repositories
{
    [CollectionDefinition(nameof(ConsolidatedInvestmentRepositoryCollection))]
    public class ConsolidatedInvestmentRepositoryCollection : ICollectionFixture<ConsolidatedInvestmentRepositoryTestsFixture> { }
    public class ConsolidatedInvestmentRepositoryTestsFixture : IDisposable
    {
        public ConsolidatedInvestmentRepository ConsolidatedInvestmentRepository;
        public AutoMocker Mocker;

        public ConsolidatedInvestmentRepository CreateConsolidatedInvestmentRepository()
        {
            Mocker = new AutoMocker();
            ConsolidatedInvestmentRepository = Mocker.CreateInstance<ConsolidatedInvestmentRepository>();
            return ConsolidatedInvestmentRepository;
        }

        public IEnumerable<TreasuryDirect> GenerateTresuryDirectListOk()
        {
            return new List<TreasuryDirect>
            {
                new TreasuryDirect
                (
                    investedValue: 799.4720m,
                    totalValue: 829.68m,
                    dueDate: new DateTime(2025, 03, 01),
                    purchaseDate: new DateTime(2015, 03, 01),
                    name: "Tesouro Selic 2025"
                )
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
                new FixedIncome
                (
                    investedValue: 2000.0m,
                    totalValue: 2097.85m,
                    dueDate: new DateTime(2021, 03, 09),
                    purchaseDate: new DateTime(2019, 03, 14),
                    name: "BANCO MAXIMA"
                )
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
                new InvestmentFund
                (
                    investedValue: 1000.0m,
                    totalValue: 1159m,
                    dueDate: new DateTime(2022, 10, 01),
                    purchaseDate: new DateTime(2017, 10, 01),
                    name: "ALASKA"
                )
            };
        }
        public IEnumerable<InvestmentFund> GenerateInvestmentFundListNotOk()
        {
            return new List<InvestmentFund>();
        }

        public void Dispose()
        {
        }
    }
}
