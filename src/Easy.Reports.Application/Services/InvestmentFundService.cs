using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Services
{
    public class InvestmentFundService : IInvestmentFundService
    {
        private readonly IMockService _mockService;
        public InvestmentFundService(IMockService mockService)
        {
            _mockService = mockService;
        }
        public async Task<IEnumerable<InvestmentFund>> GetInvestmentFund(DateTime dataResgate)
        {
            var investmentFundMockModel = await _mockService.GetInvestmentFundAsync();
            List<InvestmentFund> fundos = new List<InvestmentFund>();

            foreach (var investmentFundMock in investmentFundMockModel.fundos)
            {
                var fundo = (InvestmentFund)investmentFundMock;
                fundo.EfetuarCalculosResgate(dataResgate);
                fundos.Add(fundo);
            }

            return fundos;
        }
    }
}
