using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public class ConsolidatedReportResponse
    {
        public decimal valorTotal { get; set; }
        public IEnumerable<ConsolidatedReportInvestment> investimentos { get; set; }

        public static explicit operator ConsolidatedReportResponse(string result)
        {
            return new ConsolidatedReportResponse
            {
                valorTotal = 999888m
            };
        }
    }
}
