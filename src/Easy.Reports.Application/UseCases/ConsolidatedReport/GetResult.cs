using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetResult
    {
        private const int _totalValueDecimalPlace = 2;
        private const int _investedValueDecimalPlace = 4;
        private const int _investmentTotalValueDecimalPlace = 2;
        private const int _irTaxValueDecimalPlace = 4;
        private const int _rescueValueDecimalPlace = 3;
        public decimal ValorTotal
        {
            get
            {
                return Math.Round(investimentos?.Sum(i => i.valorTotal) ?? 0, _totalValueDecimalPlace);
            }
        }

        public IEnumerable<GetResultInvestments> investimentos { get; set; }

        public static explicit operator GetResult(List<Investment> investments)
        {
            var investimentos = new List<GetResultInvestments>();

            foreach (var investment in investments)
            {
                investimentos.Add(
                    new GetResultInvestments
                    {
                        nome = investment.Name,
                        valorInvestido = Math.Round(investment.InvestedValue, _investedValueDecimalPlace),
                        valorTotal = Math.Round(investment.TotalValue, _investmentTotalValueDecimalPlace),
                        vencimento = investment.DueDate,
                        ir = Math.Round(investment.IrTaxValue, _irTaxValueDecimalPlace),
                        valorResgate = Math.Round(investment.RescueValue, _rescueValueDecimalPlace)
                    }
                );
            }

            return new GetResult
            {
                investimentos = investimentos
            };
        }
    }
}
