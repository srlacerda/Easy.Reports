using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetResult
    {
        public decimal TotalValue { get; set; }
        public IEnumerable<Investment> Investments { get; set; }
    }
}
