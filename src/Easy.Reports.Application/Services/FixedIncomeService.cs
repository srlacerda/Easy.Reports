using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IEnumerable<FixedIncome>> GetFixedIncome(DateTime dataResgate)
        {
            var fixedIncomeMockModel = await _mockService.GetFixedIncomeAsync();
            List<FixedIncome> lcis = new List<FixedIncome>();

            foreach (var fixedIncomeMock in fixedIncomeMockModel.lcis)
            {
                var lci = (FixedIncome)fixedIncomeMock;
                lci.EfetuarCalculosResgate(dataResgate);
                lcis.Add(lci);
            }

            return lcis;
        }
    }
}
