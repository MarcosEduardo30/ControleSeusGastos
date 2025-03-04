using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.BuscarDespesa;
using Application.Services.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.CriarDespesa;
using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Services.Despesas.EditarDespesa;
using Application.Services.Despesas.EditarDespesa.DTO;
using Application.Services.Despesas.ExcluirDespesa;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Mvc;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class Despesas : Controller
    {
        private readonly ICriarDespesaService _criarDespesa;
        private readonly IBuscarDespesaService _buscarDespesa;
        private readonly IEditarDespesaService _editarDespesa;
        private readonly IExcluirDespesaService _excluirDespesa;

        public Despesas(ICriarDespesaService criarDespesa,
            IBuscarDespesaService buscarDespesa,
            IEditarDespesaService editarDespesa,
            IExcluirDespesaService excluirDespesa) 
        { 
            _criarDespesa = criarDespesa;
            _buscarDespesa = buscarDespesa;
            _editarDespesa = editarDespesa;
            _excluirDespesa = excluirDespesa;
        }


        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResultadoAPI<CriarDespesaOutput>>> CriarDespesa(CriarDespesaInput input)
        {
            var novadespesa = await _criarDespesa.CriarNovaDespesa(input);
            var resultado = new ResultadoAPI<CriarDespesaOutput>(StatusResult.Success, novadespesa.Valor);
            return Ok(resultado);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<BuscarDespesaOutput>>> BuscarDespesa(int id)
        {
            var despesa = await _buscarDespesa.BuscarPorId(id);

            if (despesa == null) {
                return NotFound();
            }

            var resultado = new ResultadoAPI<BuscarDespesaOutput>(StatusResult.Success, despesa);
            return Ok(resultado);

        }

        [HttpGet("BuscarPorUsuario")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<List<BuscarDespesaOutput>>>> BuscarPorIdUsuario(int idUsuario)
        {
            var despesas = await _buscarDespesa.BuscarPorIdUsuario(idUsuario);

            if (despesas == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<List<BuscarDespesaOutput>>(StatusResult.Success, despesas);
            return Ok(resultado);
        }

        [HttpGet("BuscarPorPeriodo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<List<BuscarDespesaOutput>>>> BuscarPorPeriodo(BuscarPorPeriodoInput input)
        {
            var despesas = await _buscarDespesa.BuscarPorPeriodo(input);

            if (despesas == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<List<BuscarDespesaOutput>>(StatusResult.Success, despesas);
            return Ok(resultado);
        }

        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<EditarDespesaOutput>>> EditarDespesa(EditarDespesaInput novaDespesa)
        {
            var despesa = await _editarDespesa.Editar(novaDespesa);

            if (despesa == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<EditarDespesaOutput>(StatusResult.Success, despesa);
            return Ok(resultado);
        }


        [HttpDelete()]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<bool>>> ExcluirDespesa(int id)
        {
            var sucess = await _excluirDespesa.Excluir(id);

            if (!sucess)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<bool>(StatusResult.Success, sucess);
            return NoContent();
        }
    }
}
