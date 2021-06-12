using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<Investment>> GetAllInvestmentsAsync(DateTime rescueDate)
        {
            var result = await Task.WhenAll(
                GetTreasuryDirectAsync(rescueDate),
                GetFixedIncomeAsync(rescueDate),
                GetInvestmentFundAsync(rescueDate)
            );
            
            var investments = result.Aggregate((r1, r2) => r1.Concat(r2));
            return investments;
        }

        private async Task<IEnumerable<Investment>> GetTreasuryDirectAsync(DateTime rescueDate)
        {
            return await _treasuryDirectService.GetTreasuryDirectAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetFixedIncomeAsync(DateTime rescueDate)
        {
            return await _fixedIncomeService.GetFixedIncomeAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetInvestmentFundAsync(DateTime rescueDate)
        {
            return await _investmentFundService.GetInvestmentFundAsync(rescueDate);
        }
    }
}
