using Easy.Reports.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class ConsolidatedInvestmentService : IConsolidatedInvestmentService
    {
        private readonly ITreasuryDirectService _treasuryDirectService;
        private readonly IFixedIncomeService _fixedIncomeService;
        private readonly IInvestmentFundService _investmentFundService;
        public ConsolidatedInvestmentService(ITreasuryDirectService treasuryDirectService, IFixedIncomeService fixedIncomeService, IInvestmentFundService investmentFundService)
        {
            _treasuryDirectService = treasuryDirectService;
            _fixedIncomeService = fixedIncomeService;
            _investmentFundService = investmentFundService;
        }
        public async Task<string> GetAllProducts(DateTime dataResgate)
        {
            var resultTreasuryDirect = await _treasuryDirectService.GetTreasuryDirect(dataResgate);

            return "diego";
        }
    }
}
