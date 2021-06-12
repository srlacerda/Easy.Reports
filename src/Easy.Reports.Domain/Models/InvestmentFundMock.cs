using System;

namespace Easy.Reports.Domain.Models
{
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
