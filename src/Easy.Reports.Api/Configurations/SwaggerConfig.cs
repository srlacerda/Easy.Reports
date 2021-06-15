using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Easy.Reports.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Easynvest Backend Test C#",
                    Version = "v1",
                    Description = "Easy Reports API Swagger",
                    Contact = new OpenApiContact()
                    {
                        Email = "diego.lacerda.alves@gmail.com",
                        Name = "Diego Lacerda Alves",
                        Url = new Uri("https://www.linkedin.com/in/diegolacerdaalves/")
                    }
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
