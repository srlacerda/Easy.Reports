using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public partial class ConsolidatedReportController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get()
        {
            var getQuery = new GetQuery(DateTime.Now);
            var response = await _mediator.Send(getQuery);
            
            if (response == null)
                return NoContent();

            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}