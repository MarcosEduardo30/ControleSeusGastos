using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Services.Despesas.EditarDespesa.DTO;
using Application.Services.Usuarios.Authentication;
using Application.Services.Usuarios.Login.DTO;
using Application.Validacao;
using ControleSeusGastos.API;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ControleSeusGastos.Integration.Tests
{
    public class EditarDespesaTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public EditarDespesaTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }


        [Fact]
        public async Task DeveriaRetonarDespesa_QueDadosValidos_AoEditarDespesa()
        {
            //arrange
            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Teste Atualizado",
                Descricao = "Descrição Atualizada",
                Categoria_id = 1,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            int id = 10;

            var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IAuthenticationService>();
            var token = await service.Login(new LoginInput() { username = "string 2", password = "string" });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                token.token);

            //act
            var response = await _httpClient.PutAsJsonAsync($"/Despesas/{id}", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<EditarDespesaOutput>>();

            //assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Null(result.erros);
            Assert.NotNull(result.data);
            Assert.Equal(100, result.data.Valor);
            Assert.Equal("Teste Atualizado", result.data.Nome);
            Assert.Equal("Descrição Atualizada", result.data.Descricao);
        }

        [Fact]
        public async Task DeveriaRetornarErro_QueIdDespesaInvalido_AoEditarDespesa()
        {
            //arrange
            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Teste Atualizado",
                Descricao = "Descrição Atualizada",
                Categoria_id = 1,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            int id = 0;

            var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IAuthenticationService>();
            var token = await service.Login(new LoginInput() { username = "string 2", password = "string" });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                token.token);

            //act
            var response = await _httpClient.PutAsJsonAsync($"/Despesas/{id}", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<EditarDespesaOutput>>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Null(result.data);
            Assert.NotNull(result.erros);
            Assert.NotEmpty(result.erros);
            Assert.Single(result.erros);
            Assert.IsType<List<Erro>>(result.erros);
            Assert.Equal("despesa_invalida", result.erros[0].nome);
        }
    }
}
