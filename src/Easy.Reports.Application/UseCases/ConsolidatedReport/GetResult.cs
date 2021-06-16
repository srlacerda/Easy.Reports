using Easy.Reports.Domain.Entities;
using Newtonsoft.Json;
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

        [JsonProperty("valorTotal")]
        public decimal TotalValue
        {
            get
            {
                return Math.Round(investimentos?.Sum(i => i.TotalValue) ?? 0, _totalValueDecimalPlace);
            }
        }
        public IList<GetResultInvestments> investimentos { get; set; }

        public GetResult(IEnumerable<Investment> investments)
        {
            this.investimentos = new List<GetResultInvestments>();
            foreach (var investment in investments)
            {
                this.investimentos.Add(
                    new GetResultInvestments
                    {
                        Name = investment.Name,
                        InvestedValue = Math.Round(investment.InvestedValue, _investedValueDecimalPlace),
                        TotalValue = Math.Round(investment.TotalValue, _investmentTotalValueDecimalPlace),
                        DueDate = investment.DueDate,
                        IrTaxValue = Math.Round(investment.IrTaxValue, _irTaxValueDecimalPlace),
                        RescueValue = Math.Round(investment.RescueValue, _rescueValueDecimalPlace)
                    }
                );
            }
        }
    }
}