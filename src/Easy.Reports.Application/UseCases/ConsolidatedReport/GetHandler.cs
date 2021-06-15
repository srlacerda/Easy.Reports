using Easy.Reports.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetHandler : IRequestHandler<GetQuery, GetResult>
    {
        private readonly string _logSucess = "Consolidated Investments got sucessfully.";
        private readonly string _logError = "An error has occurred during the request.";
        private readonly ILogger _logger;
        private readonly IConsolidatedInvestmentRepository _consolidatedInvestmentRepository;
        public GetHandler(ILogger logger, IConsolidatedInvestmentRepository consolidatedInvestmentRepository)
        {
            _logger = logger;
            _consolidatedInvestmentRepository = consolidatedInvestmentRepository;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            GetResult getResult;
            try
            {
                var investments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(request.RescueDate);
                getResult = (GetResult) investments.ToList();
                _logger.Info(_logSucess);
            }
            catch (Exception e)
            {
                _logger.Error(e, _logError);
                getResult = new GetResult();
            }

            return getResult;
        }
    }
}
