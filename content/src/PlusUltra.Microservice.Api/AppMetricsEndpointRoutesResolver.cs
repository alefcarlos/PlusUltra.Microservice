using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;

namespace PlusUltra.Microservice.Api
{
    public static class AppMetricsEndpointRoutesResolver
    {
        public static void UseAppMetricsEndpointRoutesResolver(this IApplicationBuilder app)
        {
            app.Use(((context, next) =>
            {
                var metricsCurrentRouteName = "__App.Metrics.CurrentRouteName__";
                var endpointFeature = context.Features[typeof(IEndpointFeature)] as IEndpointFeature;
                if (endpointFeature?.Endpoint is RouteEndpoint endpoint)
                {
                    var method = endpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods
                        ?.FirstOrDefault();
                    var routePattern = endpoint.RoutePattern?.RawText;
                    var templateRouteDetailed = $"{method} {routePattern} {endpoint.DisplayName}";
                    var templateRoute = $"{method} {routePattern}";
                    if (!context.Items.ContainsKey(metricsCurrentRouteName))
                    {
                        context.Items.Add(metricsCurrentRouteName, templateRoute);
                    }
                }

                return next();
            }));
        }
    }
}
