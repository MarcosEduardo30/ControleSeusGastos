using Application.Despesas.BuscarDespesa.DTO;
using ControleSeusGastos.API;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

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


        [Fact]
        public async Task DeveriaRetornarErro_QueIdInvalido_AoBuscarDespesa()
        {
            //Arrange
            int id = 0;

            //Act
            var response = await _client.GetAsync($"/Despesas/{id}");
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<BuscarDespesaOutput>>();

            //Assert
            Assert.NotNull(result);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(result.data);
        }
    }
}
