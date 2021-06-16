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
                var resultInvestments = await Task.WhenAll(
                    GetTreasuryDirectAsync(rescueDate),
                    GetFixedIncomeAsync(rescueDate),
                    GetInvestmentFundAsync(rescueDate)
                );

                investments = resultInvestments.Aggregate((r1, r2) => r1?.Concat(r2));

                if (!investments.Any())
                    return null;

                _memoryCache.Set(cashKey, investments, new MemoryCacheEntryOptions {AbsoluteExpiration = rescueDate.Date.AddDays(1)});
            }

            return investments;
        }

        private async Task<IEnumerable<Investment>> GetTreasuryDirectAsync(DateTime rescueDate)
        {
            return await _treasuryDirectRepository.GetTreasuryDirectAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetFixedIncomeAsync(DateTime rescueDate)
        {
            return await _fixedIncomeRepository.GetFixedIncomeAsync(rescueDate);
        }

        private async Task<IEnumerable<Investment>> GetInvestmentFundAsync(DateTime rescueDate)
        {
            return await _investmentFundRepository.GetInvestmentFundAsync(rescueDate);
        }
    }
}