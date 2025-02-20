using Application.Despesas.CriarDespesa;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIConfig
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICriarDespesa, CriarDespesa>();

            return services;
        }
    }
}
