using Newtonsoft.Json;
using System.Collections.Generic;

namespace Easy.Reports.Domain.Models
{
    public class TreasuryDirectMockModel
    {
        [JsonProperty("tds")]
        public IEnumerable<TreasuryDirectMock> TreasuryDirectMockList { get; set; }
    }
}