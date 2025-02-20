using Application.Despesas.CriarDespesa;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIConfig
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICriarDespesa, CriarDespesa>();
            services.AddInfrastructureServices();

            return services;
        }
    }
}
