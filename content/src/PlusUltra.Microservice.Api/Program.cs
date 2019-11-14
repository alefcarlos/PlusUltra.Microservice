using System.Linq;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PlusUltra.Microservice.Api
{
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        public static void Main(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                // .OutputMetrics.AsPrometheusProtobuf()
                .Build();

            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                        .ConfigureMetrics(Metrics)
                        .UseMetrics(
                            options =>
                            {
                                options.EndpointOptions = endpointsOptions =>
                                {
                                    // endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                    endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                };
                            })
                .UseStartup<Startup>();
        }
    }
}
