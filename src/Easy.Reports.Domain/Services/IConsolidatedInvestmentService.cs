using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface IConsolidatedInvestmentService
    {
        Task<string> GetAllProducts(DateTime dataResgate);
    }
}
