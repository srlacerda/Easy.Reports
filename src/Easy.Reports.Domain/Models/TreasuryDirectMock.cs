using Newtonsoft.Json;
using System;

namespace Easy.Reports.Domain.Models
{
    public class TreasuryDirectMock 
    {
        [JsonProperty("valorInvestido")]
        public decimal InvestedValue { get;  set; }

        [JsonProperty("valorTotal")]
        public decimal TotalValue { get;  set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get;  set; }

        [JsonProperty("dataDeCompra")]
        public DateTime PurchaseDate { get;  set; }

        [JsonProperty("iof")]
        public decimal Iof { get;  set; }

        [JsonProperty("indice")]
        public string Index { get;  set; }

        [JsonProperty("tipo")]
        public string InvestmentType { get;  set; }

        [JsonProperty("nome")]
        public string Name { get;  set; }
    }
}
