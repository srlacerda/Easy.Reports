using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class InvestmentFundService : IInvestmentFundService
    {
        private readonly IMockService _mockService;
        public InvestmentFundService(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<InvestmentFund>> GetInvestmentFundAsync(DateTime rescueDate)
        {
            var investmentFundMockModel = await _mockService.GetInvestmentFundAsync();
            var investmentFundList = new List<InvestmentFund>();

            if (investmentFundMockModel.IsSuccessStatusCode)
            {
                foreach (var investmentFundMock in investmentFundMockModel.Content.fundos)
                {
                    var investmentFund = (InvestmentFund)investmentFundMock;
                    investmentFund.PerformCalculationsRescue(rescueDate);
                    investmentFundList.Add(investmentFund);
                }
            }
            return investmentFundList;
        }
    }
}
