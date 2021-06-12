using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface IConsolidatedInvestmentService
    {
        Task<IEnumerable<Investment>> GetAllInvestmentsAsync(DateTime rescueDate);
    }
}
