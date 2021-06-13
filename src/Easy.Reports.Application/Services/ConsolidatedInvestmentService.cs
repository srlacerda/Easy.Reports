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
        public async Task<IEnumerable<Investment>> GetAllCalculatedInvestmentsAsync(DateTime rescueDate)
        {
            var result = await Task.WhenAll(
                GetCalculatedTreasuryDirectAsync(rescueDate),
                GetCalculatedFixedIncomeAsync(rescueDate),
                GetCalculatedInvestmentFundAsync(rescueDate)
            );
            
            var investments = result.Aggregate((r1, r2) => r1.Concat(r2));
            return investments;
        }

        private async Task<IEnumerable<Investment>> GetCalculatedTreasuryDirectAsync(DateTime rescueDate)
        {
            return await _treasuryDirectService.GetCalculatedTreasuryDirectAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetCalculatedFixedIncomeAsync(DateTime rescueDate)
        {
            return await _fixedIncomeService.GetCalculatedFixedIncomeAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetCalculatedInvestmentFundAsync(DateTime rescueDate)
        {
            return await _investmentFundService.GetCalculatedInvestmentFundAsync(rescueDate);
        }
    }
}
