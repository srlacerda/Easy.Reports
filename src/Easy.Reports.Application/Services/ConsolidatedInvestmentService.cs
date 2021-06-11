using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<Investment>> GetAllProducts(DateTime dataResgate)
        {
            //var resultTreasuryDirect = await _treasuryDirectService.GetTreasuryDirect(dataResgate);
            //var resultfixedIncome = await _fixedIncomeService.GetFixedIncome(dataResgate);
            //var resultinvestmentFund = await _investmentFundService.GetInvestmentFund(dataResgate);
            
            var result = await Task.WhenAll(
                GetTreasuryDirect(dataResgate),
                GetFixedIncome(dataResgate),
                GetInvestmentFund(dataResgate)
            );

            var investments = result.Aggregate((r1, r2) => r1.Concat(r2));
            return investments;
        }

        private async Task<IEnumerable<Investment>> GetTreasuryDirect(DateTime dataResgate)
        {
            return await _treasuryDirectService.GetTreasuryDirect(dataResgate);
        }

        private async Task<IEnumerable<Investment>> GetFixedIncome(DateTime dataResgate)
        {
            return await _fixedIncomeService.GetFixedIncome(dataResgate);
        }

        private async Task<IEnumerable<Investment>> GetInvestmentFund(DateTime dataResgate)
        {
            return await _investmentFundService.GetInvestmentFund(dataResgate);
        }
    }
}
