using Easy.Reports.Domain.Services;
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
        private const string cashKey = "InvestmentsDay";
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

            ////var chaveDoCache = "chave_do_cache";
            //string dataAtual;

            //if (!_memoryCache.TryGetValue(cashKey, out dataAtual))
            //{
            //    var opcoesDoCache = new MemoryCacheEntryOptions()
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddSeconds(500)
            //    };
            //    dataAtual = DateTime.Now.ToString();
            //    //_memoryCache.Set(chaveDoCache, dataAtual, opcoesDoCache);
            //    _memoryCache.Set(cashKey, dataAtual, opcoesDoCache);
            //}

            var resultConsolidatedInvesment = _consolidatedInvestmentService.GetAllProducts(request.dateRequest);

            
            var result = new GetResult
            {
                valorTotal = 10
            };

            return await Task.FromResult(result);
        }
    }
}
