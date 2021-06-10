using Easy.Reports.Domain.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Reports.Application.Client
{
    public interface IMock
    {
        ///5e3429a33000008c00d96336
        //

        //Task<FixedIncomeModel> GetFixedIncome();
        //Task<object> GetFixedIncome();
        [Get("/5e3429a33000008c00d96336")]
        Task<FixedIncomeModel> GetFixedIncome();
        //Task<IEnumerable<FixedIncome>> GetFixedIncome();

    }
}
