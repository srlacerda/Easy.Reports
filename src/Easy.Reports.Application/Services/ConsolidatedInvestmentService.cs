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
        public async Task<IEnumerable<Investment>> GetAllProducts(DateTime dataResgate)
        //public async Task<GetResult> GetAllProducts(DateTime dataResgate, CancellationToken cancellationToken)
        {
            var result = await Task.WhenAll(
                GetTreasuryDirect(dataResgate),
                GetFixedIncome(dataResgate),
                GetInvestmentFund(dataResgate)
            );
            
            var investments = result.Aggregate((r1, r2) => r1.Concat(r2));
            return investments;
            //GetResult getResult = new _GetResult
            //{
            //    valorTotal = investments.Sum(i => i.valorResgate),
            //    investments = investments
            //};

            //return getResult;
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
