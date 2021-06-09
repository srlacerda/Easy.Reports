using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    //Tesouro Direto - tds
    public class TreasuryDirect
    {
        public decimal valorInvestido { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime vencimento { get; set; }
        public DateTime dataDeCompra { get; set; }
        public decimal iof { get; set; }
        public string indice { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }

    }
}
