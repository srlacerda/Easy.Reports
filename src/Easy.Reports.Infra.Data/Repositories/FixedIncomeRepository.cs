using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easy.Reports.Infra.Data.Repositories
{
    public class FixedIncomeRepository : IFixedIncomeRepository
    {
        private readonly IMockService _mockService;
        public FixedIncomeRepository(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<FixedIncome>> GetFixedIncomeAsync(DateTime rescueDate)
        {
            var apiResponseFixedIncomeMockModel = await _mockService.GetFixedIncomeAsync();

            if (apiResponseFixedIncomeMockModel.IsSuccessStatusCode)
                return apiResponseFixedIncomeMockModel.Content.FixedIncomeList.Select(x => (FixedIncome) x);

            return new List<FixedIncome>();
        }
    }
}