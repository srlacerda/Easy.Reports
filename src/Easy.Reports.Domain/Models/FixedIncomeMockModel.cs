using Newtonsoft.Json;
using System.Collections.Generic;

namespace Easy.Reports.Domain.Models
{
    public class FixedIncomeMockModel
    {
        [JsonProperty("lcis")]
        public IEnumerable<FixedIncomeMock> FixedIncomeList { get; set; }
    }
}