using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface IInvestmentFundService
    {
        Task<IEnumerable<InvestmentFund>> GetCalculatedInvestmentFundAsync(DateTime rescueDate);
    }
}
