using Application.Despesas.CriarDespesa;
using Application.Usuarios.BuscarUsuario;
using Application.Usuarios.CriarUsuario;
using Application.Usuarios.EditarUsuario;
using Application.Usuarios.ExcluirUsuario;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIConfig
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICriarDespesa, CriarDespesa>();
            services.AddScoped<IBuscarUsuarioService, BuscarUsuarioService>();
            services.AddScoped<ICriarUsuarioService, CriarUsuarioService>();
            services.AddScoped<IEditarUsuarioService, EditarUsuarioService>();
            services.AddScoped<IExcluirUsuarioService, ExcluirUsuarioService>();
            services.AddInfrastructureServices();

            return services;
        }
    }
}
