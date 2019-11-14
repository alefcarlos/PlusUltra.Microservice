using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlusUltra.Swagger.Extensions;
using PlusUltra.WebApi.Hosting;
using PlusUltra.Data.SqlKata.PostgresSQL;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Logging;
using Microsoft.Extensions.Hosting;
using PlusUltra.Microservice.Infrastructure.Data.Repositories;

namespace PlusUltra.Microservice.Api
{
    public class Startup : WebApiStartup
    {
        public Startup(IConfiguration configuration)
    :       base(configuration, useAuthentication: true)
        {
        }

        public override void AfterConfigureServices(IServiceCollection services)
        {
            services.AddDocumentation(new OpenApiInfo
            {
                Title = "Titulo do Micro Serviço",
                Description = "Descrição do Micro Serviço.",
            });

            services.AddPostgressSQL(Configuration);
            services.AddRepositories();

            var validators = FluentValidation.AssemblyScanner.FindValidatorsInAssembly(typeof(Startup).Assembly);
            validators.ForEach(v => services.AddTransient(v.InterfaceType, v.ValidatorType));
        }

        public override void BeforeConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UsePathBase("/v1/");
        }

        public override void AfterConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDocumentation(configuration: c =>
            {
                c.DocumentTitle = "API";
            });
        }

        public override void ConfigureAfterRouting(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAppMetricsEndpointRoutesResolver();
        }

        public override void MapEndpoints(IEndpointRouteBuilder endpoints)
        {

        }
    }
}
