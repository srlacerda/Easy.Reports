using System;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetResultInvestments
    {
        public string nome { get; set; }
        public decimal valorInvestido { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime vencimento { get; set; }
        public decimal ir { get; set; }
        public decimal valorResgate { get; set; }
    }
}
