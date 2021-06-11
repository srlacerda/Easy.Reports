using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public partial class ConsolidatedReportController : Controller
    {
        [HttpGet]
        public async Task<ConsolidatedReportResponse> Get()
        {
            //var query = new GetQuery();
            var query = new GetQuery(DateTime.Now);

            var result = await _mediator.Send(query, CancellationToken.None);
            var response = (ConsolidatedReportResponse)result;
            //var response = new ConsolidatedReportResponse();
            return await Task.FromResult(response);
        }
    }
}
