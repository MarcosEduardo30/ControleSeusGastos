using Infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
