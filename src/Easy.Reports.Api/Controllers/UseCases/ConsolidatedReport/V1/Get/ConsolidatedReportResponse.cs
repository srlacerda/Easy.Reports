using Easy.Reports.Domain.UseCases.ConsolidatedReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public class ConsolidatedReportResponse
    {
        private const int _valorTotalInvestimentosCasasDecimais = 2;
        private const int _valorInvestidoCasasDecimais = 4;
        private const int _valorTotalCasasDecimais = 2;
        private const int _irCasasDecimais = 4;
        private const int _valorResgateCasasDecimais = 3;

        public decimal valorTotal { get; set; }
        public IEnumerable<ConsolidatedReportInvestment> investimentos { get; set; }

        public static explicit operator ConsolidatedReportResponse(GetResult getResult)
        {
            var lista = new List<ConsolidatedReportInvestment>();

            foreach (var investment in getResult.investments)
            {
                lista.Add(
                    new ConsolidatedReportInvestment
                    {
                        nome= investment.nome,
                        valorInvestido = Math.Round(investment.valorInvestido, _valorInvestidoCasasDecimais),
                        valorTotal = Math.Round(investment.valorTotal, _valorTotalCasasDecimais),
                        vencimento = investment.vencimento,
                        ir = Math.Round(investment.ir, _irCasasDecimais),
                        valorResgate = Math.Round(investment.valorResgate, _valorResgateCasasDecimais)
                    }
                );
            }

            return new ConsolidatedReportResponse
            {
                valorTotal = Math.Round(getResult.valorTotal, _valorTotalInvestimentosCasasDecimais),
                investimentos = lista
            };
        }
    }
}
