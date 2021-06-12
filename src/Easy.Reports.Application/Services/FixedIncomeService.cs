using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class FixedIncomeService : IFixedIncomeService
    {
        private readonly IMockService _mockService;
        public FixedIncomeService(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<FixedIncome>> GetFixedIncomeAsync(DateTime rescueDate)
        {
            var fixedIncomeMockModel = await _mockService.GetFixedIncomeAsync();
            var fixedIncomeList = new List<FixedIncome>();

            if (fixedIncomeMockModel.IsSuccessStatusCode)
            {
                foreach (var fixedIncomeMock in fixedIncomeMockModel.Content.lcis)
                {
                    var fixedIncome = (FixedIncome)fixedIncomeMock;
                    fixedIncome.PerformCalculationsRescue(rescueDate);
                    fixedIncomeList.Add(fixedIncome);
                }
            }
            return fixedIncomeList;
        }
    }
}
