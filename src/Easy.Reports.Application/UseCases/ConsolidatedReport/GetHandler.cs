using Easy.Reports.Domain.Services;
using Easy.Reports.Domain.UseCases.ConsolidatedReport;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
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
            GetResult getResult;

            DateTime diaSeguinte = request.dateRequest.Date.AddDays(1);
            if (!_memoryCache.TryGetValue(cashKey, out getResult))
            {
                var opcoesDoCache = new MemoryCacheEntryOptions()
                {
                    //AbsoluteExpiration = DateTime.Now.AddSeconds(500)
                    AbsoluteExpiration = diaSeguinte
                };
                
                getResult = await _consolidatedInvestmentService.GetAllProducts(request.dateRequest);
                _memoryCache.Set(cashKey, getResult, opcoesDoCache);
            }

            return getResult;
        }
    }
}
