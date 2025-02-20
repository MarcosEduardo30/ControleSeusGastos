using Infrastructure.Db;
using Infrastructure.Repositories.Depesas;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();

            return services;
        }
    }
}
