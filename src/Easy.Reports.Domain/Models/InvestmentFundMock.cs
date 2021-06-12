using Newtonsoft.Json;
using System;

namespace Easy.Reports.Domain.Models
{
    public class InvestmentFundMock
    {

        [JsonProperty("capitalInvestido")]
        public decimal InvestedCapital { get; set; }

        [JsonProperty("ValorAtual")]
        public decimal CurrentValue { get; set; }

        [JsonProperty("dataResgate")]
        public DateTime RescueDate { get; set; }

        [JsonProperty("dataCompra")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public decimal Iof { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("totalTaxas")]
        public decimal TotalFees { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
