using Application.Despesas.CriarDespesa;
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
        public async Task<ActionResult<CriarDespesaOutput>> CriarDespesa(CriarDespesaDTO input)
        {
            var novadespesa = await _criarDespesa.CriarNovaDespesa(input);
            return Ok(novadespesa);
        }
    }
}
