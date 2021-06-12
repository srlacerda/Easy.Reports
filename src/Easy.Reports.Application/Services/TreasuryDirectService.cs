using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class TreasuryDirectService : ITreasuryDirectService
    {
        private readonly IMockService _mockService;
        public TreasuryDirectService(IMockService mockService)
        {
            _mockService = mockService;
        }

        public async Task<IEnumerable<TreasuryDirect>> GetTreasuryDirect(DateTime dataResgate)
        {
            var treasuryDirectMockModel = await _mockService.GetTreasuryDirectAsync();
            var tds = new List<TreasuryDirect>();

            if (treasuryDirectMockModel.IsSuccessStatusCode)
            {
                foreach (var treasuryDirectMock in treasuryDirectMockModel.Content.tds)
                {
                    var td = (TreasuryDirect)treasuryDirectMock;
                    td.EfetuarCalculosResgate(dataResgate);
                    tds.Add(td);
                }
            }
            
            return tds;
           
        }
    }
}
