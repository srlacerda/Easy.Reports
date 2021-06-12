using Easy.Reports.Application.UseCases.ConsolidatedReport;
using System;
using System.Collections.Generic;

namespace Easy.Reports.Api.Controllers.UseCases.ConsolidatedReport.V1
{
    public class ConsolidatedReportResponse
    {
        private const int _totalValueDecimalPlace = 2;
        private const int _investedValueDecimalPlace = 4;
        private const int _investmentTotalValueDecimalPlace = 2;
        private const int _irTaxValueDecimalPlace = 4;
        private const int _rescueValueDecimalPlace = 3;
        public decimal valorTotal { get; set; }
        public IEnumerable<ConsolidatedReportInvestment> investimentos { get; set; }

        public static explicit operator ConsolidatedReportResponse(GetResult getResult)
        {
            var investments = new List<ConsolidatedReportInvestment>();

            foreach (var investment in getResult.Investments)
            {
                investments.Add(
                    new ConsolidatedReportInvestment
                    {
                        nome= investment.Name,
                        valorInvestido = Math.Round(investment.InvestedValue, _investedValueDecimalPlace),
                        valorTotal = Math.Round(investment.TotalValue, _investmentTotalValueDecimalPlace),
                        vencimento = investment.DueDate,
                        ir = Math.Round(investment.IrTaxValue, _irTaxValueDecimalPlace),
                        valorResgate = Math.Round(investment.RescueValue, _rescueValueDecimalPlace)
                    }
                );
            }

            return new ConsolidatedReportResponse
            {
                valorTotal = Math.Round(getResult.TotalValue, _totalValueDecimalPlace),
                investimentos = investments
            };
        }
    }
}
