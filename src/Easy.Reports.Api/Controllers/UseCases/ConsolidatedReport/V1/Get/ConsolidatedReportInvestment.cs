using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public class ConsolidatedReportInvestment
    {
        public string nome { get; set; }
        public decimal valorInvestido { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime vencimento { get; set; }
        public decimal ir { get; set; }
        public decimal valorResgate { get; set; }
    }
}
