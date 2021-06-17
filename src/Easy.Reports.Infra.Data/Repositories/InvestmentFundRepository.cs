using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<InvestmentFund>> GetInvestmentFundAsync()
        {
            var apiResponseInvestmentFundMockModel = await _mockService.GetInvestmentFundAsync();

            if (apiResponseInvestmentFundMockModel.IsSuccessStatusCode)
                return apiResponseInvestmentFundMockModel.Content.InvestmentFundMockList.Select(x => new InvestmentFund(x));

            return new List<InvestmentFund>();
        }
    }
}