using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Validacao;
using ControleSeusGastos.API;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
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
            _httpClient = _factory.CreateClient();
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

        [Fact]
        public async Task DeveriaRetornarErro_QueDadosInvalidos_AoCriarDespesa()
        {
            //arrange
            var input = new CriarDespesaInput
            {
                Valor = -100,
                Nome = "",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/Despesas", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<CriarDespesaOutput>>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Null(result.data);
            Assert.NotNull(result.erros);
            Assert.NotEmpty(result.erros);
            Assert.IsType<List<Erro>>(result.erros);
        }

        [Fact]
        public async Task DeveriaRetornarErro_QueUsuarioInvalido_AoCriarDespesa()
        {
            //arrange
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 0,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/Despesas", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<CriarDespesaOutput>>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Null(result.data);
            Assert.NotNull(result.erros);
            Assert.NotEmpty(result.erros);
            Assert.Single(result.erros);
            Assert.IsType<List<Erro>>(result.erros);
            Assert.Equal("Usuario_Invalido", result.erros[0].nome);
        }

        [Fact]
        public async Task DeveriaRetornarErro_QueCategoriaInvalida_AoCriarDespesa()
        {
            //arrange
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 0,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01).ToUniversalTime()
            };

            //act
            var response = await _httpClient.PostAsJsonAsync("/Despesas", input);
            var result = await response.Content.ReadFromJsonAsync<ResultadoAPI<CriarDespesaOutput>>();

            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Null(result.data);
            Assert.NotNull(result.erros);
            Assert.NotEmpty(result.erros);
            Assert.Single(result.erros);
            Assert.IsType<List<Erro>>(result.erros);
            Assert.Equal("Categoria_Invalida", result.erros[0].nome);
        }
    }
}
