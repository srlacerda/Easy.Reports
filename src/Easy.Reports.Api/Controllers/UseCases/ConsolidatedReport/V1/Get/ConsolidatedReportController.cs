using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public partial class ConsolidatedReportController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GetResult> Get()
        {
            var getQuery = new GetQuery(DateTime.Now);
            return await _mediator.Send(getQuery, CancellationToken.None);
        }
    }
}
