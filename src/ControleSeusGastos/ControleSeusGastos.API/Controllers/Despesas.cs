using Application.Despesas.CriarDespesa;
using Application.Despesas.CriarDespesa.DTO;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Mvc;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class Despesas : Controller
    {
        private readonly ICriarDespesa _criarDespesa;

        public Despesas(ICriarDespesa criarDespesa) { 
            _criarDespesa = criarDespesa;
        }


        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResultadoAPI<CriarDespesaDTO>>> CriarDespesa(CriarDespesaDTO input)
        {
            var novadespesa = await _criarDespesa.CriarNovaDespesa(input);
            var resultado = new ResultadoAPI<CriarDespesaDTO>(StatusResult.Success, novadespesa);
            return Ok(resultado);
        }
    }
}
