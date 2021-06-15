using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Interfaces
{
    public interface IFixedIncomeRepository
    {
        Task<IEnumerable<FixedIncome>> GetFixedIncomeAsync(DateTime rescueDate);
    }
}
