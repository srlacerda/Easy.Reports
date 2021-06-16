using Easy.Reports.Application.UseCases.ConsolidatedReport;
using Easy.Reports.Domain.Interfaces;
using Easy.Reports.Infra.CrossCutting.Log;
using Easy.Reports.Infra.Data.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Easy.Reports.Infra.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMediatr();
            services.RegisterCaching();
            services.RegisterExternalServices(configuration);
            services.RegisterRepositories();
            services.RegisterLogger();
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

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IConsolidatedInvestmentRepository, ConsolidatedInvestmentRepository>();
            services.AddTransient<ITreasuryDirectRepository, TreasuryDirectRepository>();
            services.AddTransient<IFixedIncomeRepository, FixedIncomeRepository>();
            services.AddTransient<IInvestmentFundRepository, InvestmentFundRepository>();
        }

        private static void RegisterLogger(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger>();
        }
    }
}