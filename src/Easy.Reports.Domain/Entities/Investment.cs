using System;

namespace Easy.Reports.Domain.Entities
{
    public abstract class Investment
    {
        private const decimal _lessThanHalfTimeInCustody = 0.30m; //_menosDaMetadeDoTempoEmCustodia
        private const decimal _moreThanHalfTimeInCustody = 0.15m; //_maisDaMetadeDoTempoEmCustodia
        private const decimal _untilThreeMonthsToDueDate = 0.06m; //_ateTresMesesParaVencer
        private const decimal _moreOrEqualToDueDate = 0m; //_maiorIgualVencimento

        public decimal InvestedValue { get; protected set; } //ValorInvestido
        public decimal TotalValue { get; protected set; } //ValorTotal
        public DateTime DueDate { get; protected set; } //Vencimento
        public DateTime PurchaseDate { get; protected set; } //DataDeCompra
        public string Name { get; protected set; } //Nome
        public decimal IrTaxValue { get; protected set; } //Ir
        public decimal RescueValue { get; protected set; } //ValorResgate

       
        public abstract void PerformCalculationsRescue(DateTime rescueDate); //EfetuarCalculosResgate (dataResgate)
        protected void PerformCalculations(DateTime rescueDate, decimal irTaxPercentage) ////EfetuarCalculos (dataResgate, irPercentual)
        {
            CalculateRescueValue(rescueDate);
            CalculateIrTax(irTaxPercentage);
        }

        private void CalculateIrTax(decimal irTaxPercentage) //CalcularIr (irPercentual)
        {
            decimal profitabilityValue = TotalValue - InvestedValue;
            IrTaxValue = irTaxPercentage * profitabilityValue;
        }

        private void CalculateRescueValue(DateTime dataResgate) //CalcularValorResgate (dataResgate)
        {
            decimal lossPercentage; //perdaPecentual

            var periodoInvestimento = DueDate.Subtract(PurchaseDate); //periodoInvestimento
            var metadePeriodoInvestimento = periodoInvestimento.Days / 2; //metadePeriodoInvestimento
            var periodoPassadoAteHoje = dataResgate.Subtract(PurchaseDate); //periodoPassadoAteHoje

            if (dataResgate >= DueDate)
            {
                lossPercentage = _moreOrEqualToDueDate;
            }
            else if (dataResgate >= DueDate.AddMonths(-3))
            {
                lossPercentage = _untilThreeMonthsToDueDate;
            }
            else if (periodoPassadoAteHoje.Days >= metadePeriodoInvestimento)
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
