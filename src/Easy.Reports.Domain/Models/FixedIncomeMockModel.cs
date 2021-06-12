using Newtonsoft.Json;
using System.Collections.Generic;

namespace Easy.Reports.Domain.Models
{
    public class FixedIncomeMockModel
    {
        //public IEnumerable<FixedIncomeMock> lcis { get; set; }
        [JsonProperty("lcis")]
        public IEnumerable<FixedIncomeMock> FixedIncomeList { get; set; }
    }
}
