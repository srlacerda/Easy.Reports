using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    [ApiController]
    [Route("[controller]")]
    public partial class ConsolidatedReportController : Controller
    {
        private readonly IMediator _mediator;
        public ConsolidatedReportController(ILogger<ConsolidatedReportController> logger, IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
