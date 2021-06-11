using Easy.Reports.Domain.Entities;
using Easy.Reports.Domain.UseCases.ConsolidatedReport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Reports.Domain.Services
{
    public interface IConsolidatedInvestmentService
    {
        //Task<GetResult> GetAllProducts(DateTime dataResgate);
        Task<GetResult> GetAllProducts(DateTime dataResgate, CancellationToken cancellationToken);
    }
}
