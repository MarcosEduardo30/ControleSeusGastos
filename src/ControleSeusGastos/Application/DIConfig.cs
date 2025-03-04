using Application.Despesas.BuscarDespesa;
using Application.Despesas.CriarDespesa;
using Application.Despesas.EditarDespesa;
using Application.Despesas.ExcluirDespesa;
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
            services.AddScoped<ICriarDespesaService, CriarDespesaService>();
            services.AddScoped<IBuscarDespesaService, BuscarDespesaService>();
            services.AddScoped<IEditarDespesaService, EditarDespesaService>();
            services.AddScoped<IExcluirDespesaService, ExcluirDespesaService>();
            services.AddScoped<IBuscarUsuarioService, BuscarUsuarioService>();
            services.AddScoped<ICriarUsuarioService, CriarUsuarioService>();
            services.AddScoped<IEditarUsuarioService, EditarUsuarioService>();
            services.AddScoped<IExcluirUsuarioService, ExcluirUsuarioService>();

            services.AddScoped<CriarDespesaValidador>();

            services.AddInfrastructureServices();

            return services;
        }
    }
}
