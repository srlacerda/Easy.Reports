using Newtonsoft.Json;
using System;

namespace Easy.Reports.Domain.Models
{
    public class FixedIncomeMock
    {
        [JsonProperty("capitalInvestido")]
        public decimal InvestedCapital { get; set; }

        [JsonProperty("capitalAtual")]
        public decimal CurrentCapital { get; set; }

        [JsonProperty("quantidade")]
        public decimal Quantitiy { get; set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get; set; }

        [JsonProperty("iof")]
        public decimal Iof { get; set; }

        [JsonProperty("outrasTaxas")]
        public decimal OtherFees { get; set; }

        [JsonProperty("taxas")]
        public decimal Fees { get; set; }
        
        [JsonProperty("indice")]
        public string Index { get; set; }

        [JsonProperty("tipo")]
        public string InvestmentType { get; set; }
        
        [JsonProperty("nome")]
        public string Name { get; set; }
        
        [JsonProperty("guarantidoFGC")]
        public bool GuaranteedFgc { get; set; }
        
        [JsonProperty("dataOperacao")]
        public DateTime OperationDate { get; set; }
        
        [JsonProperty("precoUnitario")]
        public decimal UnitPrice { get; set; }
        
        [JsonProperty("primario")]
        public bool Primary { get; set; }
    }
}