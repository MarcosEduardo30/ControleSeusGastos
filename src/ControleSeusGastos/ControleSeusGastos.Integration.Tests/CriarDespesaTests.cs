using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Services.Usuarios.Authentication;
using Application.Services.Usuarios.Login.DTO;
using Application.Validacao;
using ControleSeusGastos.API;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ControleSeusGastos.Integration.Tests
{
    public class CriarDespesaTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public CriarDespesaTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(opt =>
                    {
                        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task DeveriaCriarDespesa_QueDadosValidos_AoCriarDespesa()
        {
            //arrange
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IAuthenticationService>();
            var token = await service.Login(new LoginInput() { username= "string 2", password= "string" });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                token);

            //act
            var response = await _httpClient.PostAsJsonAsync("/Despesas", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<CriarDespesaOutput>>();

            //assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Null(result.erros);
            Assert.NotNull(result.data);
            Assert.Equal(100, result.data.Valor);
            Assert.Equal("Teste", result.data.Nome);
            Assert.Equal("Descrição teste", result.data.Descricao);
        }
    }
}
