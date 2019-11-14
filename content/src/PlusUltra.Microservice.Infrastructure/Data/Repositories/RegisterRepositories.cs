using Microsoft.Extensions.DependencyInjection;

namespace PlusUltra.Microservice.Infrastructure.Data.Repositories
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
