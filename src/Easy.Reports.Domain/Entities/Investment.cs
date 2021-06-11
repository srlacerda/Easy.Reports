using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public abstract class Investment 
    {
        private const decimal _menosDaMetadeDoTempoEmCustodia = 0.30m;
        private const decimal _maisDaMetadeDoTempoEmCustodia = 0.15m;
        private const decimal _ateTresMesesParaVencer = 0.06m;
        private const decimal _maiorIgualVencimento = 0m;

        protected decimal CalcularIr(decimal irPercentual, decimal valorTotal, decimal valorInvestido)
        {
            decimal valorRentabilidade = valorTotal - valorInvestido;
            return irPercentual * valorRentabilidade;
        }
        
        protected decimal CalcularValorResgate(DateTime dataResgate, DateTime dataDeCompra, DateTime vencimento, decimal valorTotal)
        {
            decimal perdaPecentual;
            
            var periodoInvestimento = vencimento.Subtract(dataDeCompra);
            var metadePeriodoInvestimento = periodoInvestimento.Days / 2;
            var periodoPassadoAteHoje = dataResgate.Subtract(dataDeCompra);

            if (dataResgate >= vencimento)
            {
                perdaPecentual = _maiorIgualVencimento;
            }
            else if (dataResgate > vencimento.AddMonths(-3))
            {
                perdaPecentual = _ateTresMesesParaVencer;
            }
            else if (periodoPassadoAteHoje.Days > metadePeriodoInvestimento)
            {
                perdaPecentual = _maisDaMetadeDoTempoEmCustodia;
            }
            else
            {
                perdaPecentual = _menosDaMetadeDoTempoEmCustodia;
            }

            return valorTotal - (perdaPecentual * valorTotal);
        }

       
    }
}
