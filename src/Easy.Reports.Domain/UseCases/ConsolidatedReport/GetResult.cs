using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.UseCases.ConsolidatedReport
{
    public class GetResult
    {
        public decimal valorTotal { get; set; }
        public IEnumerable<Investment> investments { get; set; }
    }
}
