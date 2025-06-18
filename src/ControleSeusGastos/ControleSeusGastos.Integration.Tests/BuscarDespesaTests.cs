using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Usuarios.Login.DTO;
using ControleSeusGastos.API;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using Application.Services.Usuarios.Authentication;

namespace ControleSeusGastos.Integration.Tests
{
    public class BuscarDespesaTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public BuscarDespesaTests(WebApplicationFactory<Program> factory) { 
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task DeveriaRetornarDespesa_QueIdValido_AoBuscarDespesa()
        {
            //Arrange
            int id = 10;

            var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IAuthenticationService>();
            var token = await service.Login(new LoginInput() { username = "string 2", password = "string" });

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                token.token);

            //Act
            var response = await _client.GetAsync($"/Despesas/{id}");
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<BuscarDespesaOutput>>();

            //Assert
            Assert.NotNull(result);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Null(result.erros);
            Assert.NotNull(result.data);
            Assert.IsType<BuscarDespesaOutput>(result.data);
        }

    }
}
