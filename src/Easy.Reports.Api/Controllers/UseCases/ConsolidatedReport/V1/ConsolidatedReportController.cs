using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    [ApiController]
    [Route("[controller]")]
    public partial class ConsolidatedReportController : Controller
    {
        private readonly IMediator _mediator;
        public ConsolidatedReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
