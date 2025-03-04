﻿using Application.Services.Despesas.BuscarDespesa;
using Application.Services.Despesas.CriarDespesa;
using Application.Services.Despesas.EditarDespesa;
using Application.Services.Despesas.ExcluirDespesa;
using Application.Services.Usuarios.BuscarUsuario;
using Application.Services.Usuarios.CriarUsuario;
using Application.Services.Usuarios.EditarUsuario;
using Application.Services.Usuarios.ExcluirUsuario;
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
