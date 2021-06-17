using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<TreasuryDirect>> GetTreasuryDirectAsync()
        {
            var apiResponseTreasuryDirectMockModel = await _mockService.GetTreasuryDirectAsync();

            if (apiResponseTreasuryDirectMockModel.IsSuccessStatusCode)
                return apiResponseTreasuryDirectMockModel.Content.TreasuryDirectMockList.Select(x => new TreasuryDirect(x));

            return new List<TreasuryDirect>();
        }
    }
}