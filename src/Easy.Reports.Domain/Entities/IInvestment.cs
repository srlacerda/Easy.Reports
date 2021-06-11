using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Domain.Entities
{
    public interface IInvestment
    {
        void EfetuarCalculosResgate(DateTime dataResgate);
    }
}
