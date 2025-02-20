using Application.Despesas.CriarDespesa;
using Microsoft.AspNetCore.Mvc;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class Despesas : Controller
    {
        private readonly CriarDespesa _criarDespesa;

        public Despesas(CriarDespesa criarDespesa) { 
            _criarDespesa = criarDespesa;
        }


        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<CriarDespesaOutput> CriarDespesa(CriarDespesaInput input)
        {
            var novadespesa = _criarDespesa.CriarNovaDespesa(input);
            return Ok(novadespesa);
        }
    }
}
