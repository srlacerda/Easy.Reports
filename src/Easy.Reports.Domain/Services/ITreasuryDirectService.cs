using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface ITreasuryDirectService
    {
        //string GetTreasuryDirect();
        Task<string> GetTreasuryDirect(DateTime dataResgate);
    }
}
