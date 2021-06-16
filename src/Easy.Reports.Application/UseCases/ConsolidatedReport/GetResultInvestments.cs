using Newtonsoft.Json;
using System;

namespace Easy.Reports.Application.UseCases.ConsolidatedReport
{
    public class GetResultInvestments
    {
        [JsonProperty("nome")]
        public string Name { get; set; }
        
        [JsonProperty("valorInvestido")]
        public decimal InvestedValue { get; set; }
        
        [JsonProperty("valorTotal")]
        public decimal TotalValue { get; set; }
        
        [JsonProperty("vencimento")]
        public DateTime DueDate { get; set; }
        
        [JsonProperty("ir")]
        public decimal IrTaxValue { get; set; }
        
        [JsonProperty("valorResgate")]
        public decimal RescueValue { get; set; }
    }
}