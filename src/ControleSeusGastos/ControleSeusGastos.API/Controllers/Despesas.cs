using Microsoft.AspNetCore.Mvc;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class Despesas : Controller
    {

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CriarDespesa()
        {
            return Ok("");
        }
    }
}
