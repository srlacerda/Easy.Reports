using Easy.Reports.Application.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetHandler : IRequestHandler<GetQuery, GetResult>
    {
        private readonly IMock _mock;
        public GetHandler(IMock mock)
        {
            _mock = mock;
        }
        public async Task<GetResult> Handle(GetQuery request, CancellationToken cancellationToken)
        {

            var resultadoMock = await _mock.GetFixedIncome();
            var result = new GetResult
            {
                valorTotal = 10
            };

            return await Task.FromResult(result);
            //return result;
        }
    }
}
