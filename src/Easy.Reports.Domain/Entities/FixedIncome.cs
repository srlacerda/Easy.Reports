using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    //Renda Fixa - lcis
    public class FixedIncome
    {
        public decimal capitalInvestido { get; set; }
        public decimal capitalAtual { get; set; }
        public decimal quantidade { get; set; }
        public DateTime vencimento { get; set; }
        public decimal iof { get; set; }
        public decimal outrasTaxas { get; set; }
        public decimal taxas { get; set; }
        public string indice { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }
        public bool guarantidoFGC { get; set; }
        public DateTime dataOperacao { get; set; }
        public decimal precoUnitario { get; set; }
        public DateTime primario { get; set; }

    }
}
