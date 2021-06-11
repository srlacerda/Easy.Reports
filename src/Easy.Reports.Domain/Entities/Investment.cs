using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public abstract class Investment //: IInvestment
    {
        private const decimal _menosDaMetadeDoTempoEmCustodia = 0.30m;
        private const decimal _maisDaMetadeDoTempoEmCustodia = 0.15m;
        private const decimal _ateTresMesesParaVencer = 0.06m;
        private const decimal _maiorIgualVencimento = 0m;

        public decimal valorInvestido { get; protected set; }
        public decimal valorTotal { get; protected set; }
        public DateTime vencimento { get; protected set; }
        public DateTime dataDeCompra { get; protected set; }
        public string nome { get; protected set; }
        public decimal ir { get; protected set; }
        public decimal valorResgate { get; protected set; }


        public abstract void EfetuarCalculosResgate(DateTime dataResgate);
        protected void EfetuarCalculos(DateTime dataResgate, decimal irPercentual)
        {
            CalcularValorResgate(dataResgate);
            CalcularIr(irPercentual);
        }

        private void CalcularIr(decimal irPercentual)
        {
            decimal valorRentabilidade = valorTotal - valorInvestido;
            ir = irPercentual * valorRentabilidade;
        }

        private void CalcularValorResgate(DateTime dataResgate)
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

            valorResgate = valorTotal - (perdaPecentual * valorTotal);
        }

       
    }
}
