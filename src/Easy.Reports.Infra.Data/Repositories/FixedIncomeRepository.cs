using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Interfaces;
using System;
using System.Collections.Generic;
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
            var fixedIncomeList = new List<FixedIncome>();

            if (apiResponseFixedIncomeMockModel.IsSuccessStatusCode)
            {
                foreach (var fixedIncomeMock in apiResponseFixedIncomeMockModel.Content.FixedIncomeList)
                {
                    fixedIncomeList.Add((FixedIncome)fixedIncomeMock);
                }
            }

            return fixedIncomeList;
        }
    }
}
