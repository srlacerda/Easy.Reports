using Easy.Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface ITreasuryDirectService
    {
        Task<IEnumerable<TreasuryDirect>> GetTreasuryDirect(DateTime dataResgate);
    }
}
