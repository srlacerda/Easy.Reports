using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetHandler : IRequestHandler<GetQuery, GetResult>
    {
        private const string cashKey = "GetConsolidatedReport";
        private readonly IMemoryCache _memoryCache;
        private readonly IMockService _mockService;
        private readonly IConsolidatedInvestmentService _consolidatedInvestmentService;
        public GetHandler(IConsolidatedInvestmentService consolidatedInvestmentService, IMemoryCache memoryCach, IMockService mockService)
        {
            _consolidatedInvestmentService = consolidatedInvestmentService;
            _memoryCache = memoryCach;
            _mockService = mockService;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
           
            if (!_memoryCache.TryGetValue(cashKey, out GetResult getResult))
            {
                var opcoesDoCache = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = request.dateRequest.Date.AddDays(1)
                };

                var investments = await _consolidatedInvestmentService.GetAllProducts(request.dateRequest);
                
                getResult = new GetResult();
                getResult.valorTotal = investments.Sum(i => i.valorResgate);
                getResult.investments = investments;

                _memoryCache.Set(cashKey, getResult, opcoesDoCache);
            }

            return getResult;
        }
    }
}
