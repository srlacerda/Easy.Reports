using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Data.Repositories
{
    public class ConsolidatedInvestmentRepository : IConsolidatedInvestmentRepository
    {
        private readonly ITreasuryDirectRepository _treasuryDirectRepository;
        private readonly IFixedIncomeRepository _fixedIncomeRepository;
        private readonly IInvestmentFundRepository _investmentFundRepository;
        public ConsolidatedInvestmentRepository(ITreasuryDirectRepository treasuryDirectRepository, IFixedIncomeRepository fixedIncomeRepository, IInvestmentFundRepository investmentFundRepository)
        {
            _treasuryDirectRepository = treasuryDirectRepository;
            _fixedIncomeRepository = fixedIncomeRepository;
            _investmentFundRepository = investmentFundRepository;
        }
        public async Task<IEnumerable<Investment>> GetAllCalculatedInvestmentsAsync(DateTime rescueDate)
        {
            var resultInvestments = await Task.WhenAll(
                GetCalculatedTreasuryDirectAsync(rescueDate),
                GetCalculatedFixedIncomeAsync(rescueDate),
                GetCalculatedInvestmentFundAsync(rescueDate)
            );

            var investments = resultInvestments.Aggregate((r1, r2) => r1.Concat(r2));
            return investments;
        }

        private async Task<IEnumerable<Investment>> GetCalculatedTreasuryDirectAsync(DateTime rescueDate)
        {
            return await _treasuryDirectRepository.GetCalculatedTreasuryDirectAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetCalculatedFixedIncomeAsync(DateTime rescueDate)
        {
            return await _fixedIncomeRepository.GetCalculatedFixedIncomeAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetCalculatedInvestmentFundAsync(DateTime rescueDate)
        {
            return await _investmentFundRepository.GetCalculatedInvestmentFundAsync(rescueDate);
        }
    }
}
