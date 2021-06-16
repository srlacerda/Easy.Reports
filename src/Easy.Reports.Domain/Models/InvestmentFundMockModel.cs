using Newtonsoft.Json;
using System.Collections.Generic;

namespace Easy.Reports.Domain.Models
{
    public class InvestmentFundMockModel
    {
        [JsonProperty("fundos")]
        public IEnumerable<InvestmentFundMock> InvestmentFundList { get; set; }
    }
}