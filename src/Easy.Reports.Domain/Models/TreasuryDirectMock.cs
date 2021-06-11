using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Models
{
    //Tesouro Direto - tds
    public class TreasuryDirectMock
    {
        public decimal valorInvestido { get;  set; }
        public decimal valorTotal { get;  set; }
        public DateTime vencimento { get;  set; }
        public DateTime dataDeCompra { get;  set; }
        public decimal iof { get;  set; }
        public string indice { get;  set; }
        public string tipo { get;  set; }
        public string nome { get;  set; }

        ////novos
        //public decimal ir { get;  set; }
        //public decimal valorResgate { get;  set; }

        //public void EfetuarCalculos()
        //{
        //    valorTotal = 1000;
        //    valorInvestido = 500;
            
        //    valorResgate = CalcularValorResgate();
        //    ir = CalcularIr();
        //}

        //private decimal CalcularIr()
        //{
        //    decimal _ir;
        //    decimal irPercentual = 0.10m;
        //    decimal valorRentabilidade = valorTotal - valorInvestido;
        //    _ir = irPercentual * valorRentabilidade;
        //    return _ir;
        //}
        //private decimal CalcularValorResgate()
        //{
        //    //IR 30 - menos da metade do tempo em custodia
        //    //IR 15 - mais da metade do tempo em custodia
        //    //IR 6 - ate 3 meses para vencer
        //    decimal _valorResgate;
        //    DateTime dataAtual = DateTime.Now;
        //    decimal perdaPecentual;

        //    var periodoInvestimento = vencimento.Subtract(dataDeCompra);
        //    var metadePeriodoInvestimento = periodoInvestimento.Days / 2;
        //    var periodoPassadoAteHoje = dataAtual.Subtract(dataDeCompra);

        //    if (dataAtual >= vencimento)
        //    {
        //        perdaPecentual = 0;
        //    }
        //    else if (dataAtual > vencimento.AddMonths(-3))
        //    {
        //        perdaPecentual = 0.06m;
        //    }
        //    else if (periodoPassadoAteHoje.Days > metadePeriodoInvestimento)
        //    {
        //        perdaPecentual = 0.15m;
        //    }
        //    else
        //    {
        //        perdaPecentual = 0.30m;
        //    }

        //    _valorResgate = valorTotal - (perdaPecentual * valorTotal);
        //    return _valorResgate;
        //}
    }
}
