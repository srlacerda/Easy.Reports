using Easy.Reports.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetHandler : IRequestHandler<GetQuery, GetResult>
    {
        private const string cashKey = "GetConsolidatedReport";
        private readonly IMemoryCache _memoryCache;
        private readonly IConsolidatedInvestmentRepository _consolidatedInvestmentRepository;
        public GetHandler(IMemoryCache memoryCach, IConsolidatedInvestmentRepository consolidatedInvestmentRepository)
        {
            _memoryCache = memoryCach;
            _consolidatedInvestmentRepository = consolidatedInvestmentRepository;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(cashKey, out GetResult getResult))
            {
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = request.RescueDate.Date.AddDays(1)
                };

                var investments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(request.RescueDate);

                getResult = new GetResult
                {
                    TotalValue = investments.Sum(i => i.RescueValue),
                    Investments = investments
                };

                _memoryCache.Set(cashKey, getResult, memoryCacheEntryOptions);
            }

            return getResult;
        }
    }
}
