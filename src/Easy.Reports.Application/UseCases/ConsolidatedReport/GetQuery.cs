using MediatR;
using System;
using System.Collections;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    //public class GetQuery : IRequest<GetResult>
    public class GetQuery : IRequest<GetResult>

    {
        public DateTime dateRequest { get; set; }

        protected GetQuery(){}
        public GetQuery(DateTime dateRequest)
        {
            this.dateRequest = dateRequest;
        }
        //public GetQuery()
        //{
        //    DateRequest = DateTime.Now;
        //}
    }
}
