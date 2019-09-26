using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlusUltra.Swagger.Extensions;
using PlusUltra.WebApi.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace PlusUltra.Microservice
{
    public class Startup : PlusUltra.WebApi.Hosting.StartupBase
    {
        public Startup(IConfiguration configuration, ILoggerFactory factory) : base(configuration, factory, useAuthentication: false)
        {
        }

        public override void AfterConfigureServices(IServiceCollection services, ILoggerFactory loggerFactory)
        {
            services.AddDocumentation(new Info
            {
                Title = "Template.Microservice"
            });
        }

        public override void BeforeConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
        }

        public override void AfterConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {

        }
    }
}
