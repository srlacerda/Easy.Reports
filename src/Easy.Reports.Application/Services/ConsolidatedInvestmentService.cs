using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class ConsolidatedInvestmentService : IConsolidatedInvestmentService
    {
        private const string cashKey = "ConsolidatedInvestment";
        private readonly IMemoryCache _memoryCache;
        private readonly ITreasuryDirectRepository _treasuryDirectRepository;
        private readonly IFixedIncomeRepository _fixedIncomeRepository;
        private readonly IInvestmentFundRepository _investmentFundRepository;
        public ConsolidatedInvestmentService(IMemoryCache memoryCache, ITreasuryDirectRepository treasuryDirectRepository, IFixedIncomeRepository fixedIncomeRepository, IInvestmentFundRepository investmentFundRepository)
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
                    GetTreasuryDirectAsync(),
                    GetFixedIncomeAsync(),
                    GetInvestmentFundAsync()
                );

                investments = resultInvestments.Aggregate((r1, r2) => r1?.Concat(r2)).ToList();

                if (!investments.Any())
                    return null;


                foreach (var investment in investments)
                    investment.PerformCalculationsRescue(rescueDate);

                _memoryCache.Set(cashKey, investments, new MemoryCacheEntryOptions { AbsoluteExpiration = rescueDate.Date.AddDays(1) });
            }

            return investments;
        }

        private async Task<IEnumerable<Investment>> GetTreasuryDirectAsync()
        {
            return await _treasuryDirectRepository.GetTreasuryDirectAsync();
        }

        private async Task<IEnumerable<Investment>> GetFixedIncomeAsync()
        {
            return await _fixedIncomeRepository.GetFixedIncomeAsync();
        }

        private async Task<IEnumerable<Investment>> GetInvestmentFundAsync()
        {
            return await _investmentFundRepository.GetInvestmentFundAsync();
        }
    }
}
