using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Easy.Reports.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
           
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddScoped<IMediator, Mediator>();
        }
    }
}
