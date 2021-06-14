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
        private readonly IConsolidatedInvestmentRepository _consolidatedInvestmentRepository;
        public GetHandler(IConsolidatedInvestmentRepository consolidatedInvestmentRepository)
        {
            _consolidatedInvestmentRepository = consolidatedInvestmentRepository;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            GetResult getResult;
            try
            {
                var investments = await _consolidatedInvestmentRepository.GetAllCalculatedInvestmentsAsync(request.RescueDate);

                getResult = new GetResult
                {
                    TotalValue = investments.Sum(i => i.RescueValue),
                    Investments = investments
                };

            }
            catch (Exception e)
            {
                getResult = new GetResult();

            }

            return getResult;

        }
    }
}
