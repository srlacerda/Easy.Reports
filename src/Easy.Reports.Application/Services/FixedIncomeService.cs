using Easy.Reports.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Reports.Application.Services
{
    public class FixedIncomeService : IFixedIncomeService
    {
        public FixedIncomeService()
        {

        }

        public string GetFixedIncome()
        {
            return "diego fixed income";
        }
    }
}
