using MediatR;
using System;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetQuery : IRequest<GetResult>
    {
        public DateTime RescueDate { get; set; }

        protected GetQuery(){}
        public GetQuery(DateTime rescueDate)
        {
           RescueDate = rescueDate;
        }
    }
}
