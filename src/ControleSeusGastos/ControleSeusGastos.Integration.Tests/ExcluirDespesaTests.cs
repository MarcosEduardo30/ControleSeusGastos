using ControleSeusGastos.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ControleSeusGastos.Integration.Tests
{
    public class ExcluirDespesaTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public ExcluirDespesaTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }



    }
}
