using MediatR;
using System;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetQuery : IRequest<GetResult>
    {
        public DateTime RescueDate { get; set; }

        public GetQuery(DateTime rescueDate)
        {
           RescueDate = rescueDate;
        }
    }
}