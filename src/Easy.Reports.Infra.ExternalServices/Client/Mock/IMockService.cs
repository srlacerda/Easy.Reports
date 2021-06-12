using Easy.Reports.Domain.Models;
using Refit;
using System.Threading.Tasks;
namespace Easy.Reports.Infra.ExternalServices.Client.Mock
{
    public interface IMockService
    {
        //[Get("/product")]
        [Get("/5e3428203000006b00d9632a")]
        Task<ApiResponse<TreasuryDirectMockModel>> GetTreasuryDirectAsync();

        //[Get("/product")]
        [Get("/5e3429a33000008c00d96336")]
        Task<FixedIncomeMockModel> GetFixedIncomeAsync();

        //[Get("/product")]
        [Get("/5e342ab33000008c00d96342")]
        Task<InvestmentFundMockModel> GetInvestmentFundAsync();
    }
}
