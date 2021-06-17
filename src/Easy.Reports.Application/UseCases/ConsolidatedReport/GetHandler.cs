using Easy.Reports.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetHandler : IRequestHandler<GetQuery, GetResult>
    {
        private readonly string _logSucess = "Consolidated Investments got sucessfully.";
        private readonly string _logError = "An error has occurred during the request.";
        private readonly string _logNull = "It was not possible to got the investments.";

        private readonly ILogger _logger;
        private readonly IConsolidatedInvestmentService _consolidatedInvestmentService;
        public GetHandler(ILogger logger, IConsolidatedInvestmentService consolidatedInvestmentService)
        {
            _logger = logger;
            _consolidatedInvestmentService = consolidatedInvestmentService;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var investments = await _consolidatedInvestmentService.GetAllCalculatedInvestmentsAsync(request.RescueDate);
                
                if (investments == null)
                {
                    _logger.Warning(_logNull);
                    return null;
                }
                
                _logger.Info(_logSucess);
                return new GetResult(investments);
            }
            catch (Exception e)
            {
                _logger.Error(e, _logError);
                return null;
            }
        }
    }
}