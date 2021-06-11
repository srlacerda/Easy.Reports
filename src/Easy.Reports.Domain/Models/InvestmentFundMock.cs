using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Models
{
    //Fundo de Investimento - fundos
    public class InvestmentFundMock
    {
        public decimal capitalInvestido { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime dataResgate { get; set; }
        public DateTime dataCompra { get; set; }
        public decimal iof { get; set; }
        public string nome { get; set; }
        public decimal totalTaxas { get; set; }
        public int quantity { get; set; }
    }
}
