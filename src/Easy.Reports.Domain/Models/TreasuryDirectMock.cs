using System;

namespace Easy.Reports.Domain.Models
{
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
    }
}
