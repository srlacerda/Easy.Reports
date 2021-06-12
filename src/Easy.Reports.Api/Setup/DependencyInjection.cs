using Easy.Reports.Application.Services;
using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Services;
using Easy.Reports.Infra.ExternalServices.Client.Mock;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Easy.Reports.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMediatr();
            services.RegisterCaching();
            services.RegisterExternalServices(configuration);
            services.RegisterInternalServices();
        }

        private static void RegisterMediatr(this IServiceCollection services)
        {
            var assembly = (typeof(GetHandler));
            services.AddMediatR(assembly);
        }
        private static void RegisterCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }
        private static void RegisterExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IMockService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ExternalMockService:BaseUrl").Value));
        }

        private static void RegisterInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IConsolidatedInvestmentService, ConsolidatedInvestmentService>();
            services.AddScoped<ITreasuryDirectService, TreasuryDirectService>();
            services.AddScoped<IFixedIncomeService, FixedIncomeService>();
            services.AddScoped<IInvestmentFundService, InvestmentFundService>();
        }
    }
}
