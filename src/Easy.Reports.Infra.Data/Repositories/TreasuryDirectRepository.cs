using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Infra.Data.Repositories
{
    public class TreasuryDirectRepository  : ITreasuryDirectRepository
    {
        private readonly IMockService _mockService;
        public TreasuryDirectRepository(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<TreasuryDirect>> GetCalculatedTreasuryDirectAsync(DateTime rescueDate)
        {
            var apiResponseTreasuryDirectMockModel = await _mockService.GetTreasuryDirectAsync();
            var treasuryDirectList = new List<TreasuryDirect>();

            if (apiResponseTreasuryDirectMockModel.IsSuccessStatusCode)
            {
                foreach (var treasuryDirectMock in apiResponseTreasuryDirectMockModel.Content.TreasuryDirectList)
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
