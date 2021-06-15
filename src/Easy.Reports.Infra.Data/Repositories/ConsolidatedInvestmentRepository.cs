using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Infra.Data.Repositories
{
    public class ConsolidatedInvestmentRepository : IConsolidatedInvestmentRepository
    {
        private const string cashKey = "ConsolidatedInvestment";
        private readonly IMemoryCache _memoryCache;
        private readonly ITreasuryDirectRepository _treasuryDirectRepository;
        private readonly IFixedIncomeRepository _fixedIncomeRepository;
        private readonly IInvestmentFundRepository _investmentFundRepository;
        public ConsolidatedInvestmentRepository(IMemoryCache memoryCache, ITreasuryDirectRepository treasuryDirectRepository, IFixedIncomeRepository fixedIncomeRepository, IInvestmentFundRepository investmentFundRepository)
        {
            _memoryCache = memoryCache;
            _treasuryDirectRepository = treasuryDirectRepository;
            _fixedIncomeRepository = fixedIncomeRepository;
            _investmentFundRepository = investmentFundRepository;
        }
        public async Task<IEnumerable<Investment>> GetAllCalculatedInvestmentsAsync(DateTime rescueDate)
        {
            if (!_memoryCache.TryGetValue(cashKey, out IEnumerable<Investment> investments))
            {
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = rescueDate.Date.AddDays(1)
                };

                var resultInvestments = await Task.WhenAll(
                    GetCalculatedTreasuryDirectAsync(rescueDate),
                    GetCalculatedFixedIncomeAsync(rescueDate),
                    GetCalculatedInvestmentFundAsync(rescueDate)
                );

                investments = resultInvestments.Aggregate((r1, r2) => r1.Concat(r2));

                _memoryCache.Set(cashKey, investments, memoryCacheEntryOptions);
            }

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
