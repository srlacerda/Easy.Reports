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
        public async Task<IEnumerable<TreasuryDirect>> GetTreasuryDirectAsync(DateTime rescueDate)
        {
            var treasuryDirectMockModel = await _mockService.GetTreasuryDirectAsync();
            var treasuryDirectList = new List<TreasuryDirect>();

            if (treasuryDirectMockModel.IsSuccessStatusCode)
            {
                foreach (var treasuryDirectMock in treasuryDirectMockModel.Content.TreasuryDirectList)
                {
                    var treasuryDirect = (TreasuryDirect)treasuryDirectMock;
                    treasuryDirect.PerformCalculationsRescue(rescueDate);
                    treasuryDirectList.Add(treasuryDirect);
                }
            }
            
            return treasuryDirectList;
           
        }
    }
}
