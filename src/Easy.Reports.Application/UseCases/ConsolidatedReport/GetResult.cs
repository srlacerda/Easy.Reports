using Easy.Reports.Domain.Entities;
using System.Collections.Generic;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetResult
    {
        public decimal TotalValue { get; set; }
        public IEnumerable<Investment> Investments { get; set; }
    }
}
