using MediatR;
using System;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetQuery : IRequest<GetResult>
    {
        public DateTime DateRequest { get; set; }
        public GetQuery()
        {
            DateRequest = DateTime.Now;
        }
        public int Id { get; set; }
    }
}
