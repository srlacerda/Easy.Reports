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
            var getQuery = new GetQuery(DateTime.Now);
            var getResult = await _mediator.Send(getQuery, CancellationToken.None);
            var consolidatedReportResponse = (ConsolidatedReportResponse)getResult;
            return await Task.FromResult(consolidatedReportResponse);
        }
    }
}
