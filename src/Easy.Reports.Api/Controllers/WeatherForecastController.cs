using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Easy.Reports.Api.UseCases.ConsolidatedReport.V1;
using Easy.Reports.Application.UseCases.ConsolidatedReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Easy.Reports.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ConsolidatedReportResponse> Get()
        {
            var query = new GetQuery();

            var result = await _mediator.Send(query, CancellationToken.None);

            var response = new ConsolidatedReportResponse();

            response.ResultadoTop = "RETORNOU";

            return await Task.FromResult(response);
        }
    }
}
