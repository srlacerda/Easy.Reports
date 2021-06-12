using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface IConsolidatedInvestmentService
    {
        //Task<GetResult> GetAllProducts(DateTime dataResgate);
        Task<IEnumerable<Investment>> GetAllProducts(DateTime dataResgate);

    }
}
