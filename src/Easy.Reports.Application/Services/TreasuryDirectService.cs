using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<string> GetTreasuryDirect(DateTime dataResgate)
        {
            var treasuryDirectMockModel = await _mockService.GetTreasuryDirectAsync();

            List<TreasuryDirect> tds = new List<TreasuryDirect>();

            foreach (var treasuryDirect in treasuryDirectMockModel.tds)
            {
                var td = (TreasuryDirect)treasuryDirect;
                td.EfetuarCalculosResgate(dataResgate);
                tds.Add(td);
            }

            //HashSet<TreasuryDirect> newTds = new HashSet<TreasuryDirect>();
            //foreach (var treasuryDirect in treasuryDirectMockModel.tds)
            //{
            //    newTds.Add((TreasuryDirect)treasuryDirect);
            //}

            return "diego TreasuryDirect";
        }
    }
}
