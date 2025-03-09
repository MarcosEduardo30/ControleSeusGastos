using ControleSeusGastos.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ControleSeusGastos.Integration.Tests
{
    internal class EditarDespesaTests
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public EditarDespesaTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }
    }
}
