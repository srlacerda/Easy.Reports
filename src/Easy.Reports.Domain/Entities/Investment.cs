using Newtonsoft.Json;
using System;

namespace Easy.Reports.Domain.Entities
{
    public abstract class Investment
    {
        private const decimal _lessThanHalfTimeInCustody = 0.30m; //_menosDaMetadeDoTempoEmCustodia
        private const decimal _moreThanHalfTimeInCustody = 0.15m; //_maisDaMetadeDoTempoEmCustodia
        private const decimal _untilThreeMonthsToDueDate = 0.06m; //_ateTresMesesParaVencer
        private const decimal _moreOrEqualToDueDate = 0m; //_maiorIgualVencimento

        [JsonProperty("valorInvestido")]
        public decimal InvestedValue { get; protected set; }

        [JsonProperty("valorTotal")]
        public decimal TotalValue { get; protected set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get; protected set; }

        [JsonProperty("dataCompra")]
        public DateTime PurchaseDate { get; protected set; }

        [JsonProperty("nome")]
        public string Name { get; protected set; }

        [JsonProperty("ir")]
        public decimal IrTaxValue { get; protected set; }

        [JsonProperty("valorResgate")]
        public decimal RescueValue { get; protected set; }

       
        public abstract void PerformCalculationsRescue(DateTime rescueDate);
        protected void PerformCalculations(DateTime rescueDate, decimal irTaxPercentage)
        {
            CalculateRescueValue(rescueDate);
            CalculateIrTax(irTaxPercentage);
        }

        private void CalculateIrTax(decimal irTaxPercentage)
        {
            decimal profitabilityValue = TotalValue - InvestedValue;
            IrTaxValue = irTaxPercentage * profitabilityValue;
        }

        private void CalculateRescueValue(DateTime dataResgate)
        {
            decimal lossPercentage; //perdaPecentual

            var investmentPeriod = DueDate.Subtract(PurchaseDate); //periodoInvestimento
            var halfInvestmentPeriod = investmentPeriod.Days / 2; //metadePeriodoInvestimento
            var untilTodayPeriod = dataResgate.Subtract(PurchaseDate); //periodoPassadoAteHoje

            if (dataResgate >= DueDate)
            {
                lossPercentage = _moreOrEqualToDueDate;
            }
            else if (dataResgate >= DueDate.AddMonths(-3))
            {
                lossPercentage = _untilThreeMonthsToDueDate;
            }
            else if (untilTodayPeriod.Days >= halfInvestmentPeriod)
            {
                lossPercentage = _moreThanHalfTimeInCustody;
            }
            else
            {
                lossPercentage = _lessThanHalfTimeInCustody;
            }

            RescueValue = TotalValue - (lossPercentage * TotalValue);
        }
    }
}
