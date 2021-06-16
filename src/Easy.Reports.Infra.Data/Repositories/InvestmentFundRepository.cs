using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Infra.Data.Repositories
{
    public class InvestmentFundRepository : IInvestmentFundRepository
    {
        private readonly IMockService _mockService;
        public InvestmentFundRepository(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<InvestmentFund>> GetInvestmentFundAsync(DateTime rescueDate)
        {
            var apiResponseInvestmentFundMockModel = await _mockService.GetInvestmentFundAsync();
            var investmentFundList = new List<InvestmentFund>();

            if (apiResponseInvestmentFundMockModel.IsSuccessStatusCode)
            {
                foreach (var investmentFundMock in apiResponseInvestmentFundMockModel.Content.InvestmentFundMockList)
                {
                    var investmentFund = new InvestmentFund(investmentFundMock);
                    investmentFund.PerformCalculationsRescue(rescueDate);
                    investmentFundList.Add(investmentFund);
                }
            }

            return investmentFundList;
        }
    }
}