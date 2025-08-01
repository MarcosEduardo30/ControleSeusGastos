using Infrastructure.Db;
using Infrastructure.Repositories.Depesas;
using Infrastructure.Repositories.RefreshTokens;
using Infrastructure.Repositories.Usuarios;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
