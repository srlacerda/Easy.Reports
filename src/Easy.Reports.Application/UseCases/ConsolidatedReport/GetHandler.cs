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
        private readonly string _logNull = "COLOCAR UMA MENSAGEM QUANDO NAO CONSEGUIR";

        private readonly ILogger _logger;
        private readonly IConsolidatedInvestmentRepository _consolidatedInvestmentRepository;
        public GetHandler(ILogger logger, IConsolidatedInvestmentRepository consolidatedInvestmentRepository)
        {
            _logger = logger;
            _consolidatedInvestmentRepository = consolidatedInvestmentRepository;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var investments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(request.RescueDate);
                
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