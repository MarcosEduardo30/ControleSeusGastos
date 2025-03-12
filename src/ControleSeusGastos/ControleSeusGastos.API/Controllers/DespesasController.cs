using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.BuscarDespesa;
using Application.Services.Despesas.CriarDespesa;
using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Services.Despesas.EditarDespesa;
using Application.Services.Despesas.EditarDespesa.DTO;
using Application.Services.Despesas.ExcluirDespesa;
using Application.Services.Usuarios.Authentication;
using ControleSeusGastos.API.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DespesasController : Controller
    {
        private readonly ICriarDespesaService _criarDespesa;
        private readonly IBuscarDespesaService _buscarDespesa;
        private readonly IEditarDespesaService _editarDespesa;
        private readonly IExcluirDespesaService _excluirDespesa;
        private IAuthenticationService _authenticationService;

        public DespesasController(ICriarDespesaService criarDespesa,
            IBuscarDespesaService buscarDespesa,
            IEditarDespesaService editarDespesa,
            IExcluirDespesaService excluirDespesa,
            IAuthenticationService authenticationService)
        {
            _criarDespesa = criarDespesa;
            _buscarDespesa = buscarDespesa;
            _editarDespesa = editarDespesa;
            _excluirDespesa = excluirDespesa;
            _authenticationService = authenticationService;
        }


        [HttpPost()]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ResultadoAPI<CriarDespesaOutput>>> CriarDespesa(CriarDespesaInput input)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if(input.Usuario_Id.ToString() != userRequestId)
            {
                return Forbid();
            }

            var novadespesa = await _criarDespesa.CriarNovaDespesa(input);
            if (novadespesa.Erros is not null)
            {
                return BadRequest(new ResultadoAPI<CriarDespesaOutput>(StatusResult.Error, null, novadespesa.Erros));
            }

            var resultado = new ResultadoAPI<CriarDespesaOutput>(StatusResult.Success, novadespesa.Valor);
            return Ok(resultado);
        }


        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<BuscarDespesaOutput>>> BuscarDespesa(int id)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (!await _authenticationService.VerificaAutorizacaoDespesa(Int32.Parse(userRequestId), id))
            {
                return Forbid();
            }

            var despesa = await _buscarDespesa.BuscarPorId(id);

            if (despesa == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<BuscarDespesaOutput>(StatusResult.Success, despesa);
            return Ok(resultado);

        }

        [HttpGet("BuscarPorUsuario/{idUsuario}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<List<BuscarDespesaOutput>>>> BuscarPorIdUsuario(int idUsuario)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (idUsuario.ToString() != userRequestId)
            {
                return Forbid();
            }

            var despesas = await _buscarDespesa.BuscarPorIdUsuario(idUsuario);
            if (despesas == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<List<BuscarDespesaOutput>>(StatusResult.Success, despesas);
            return Ok(resultado);
        }

        [HttpGet("BuscarPorPeriodo/{idUsuario}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<List<BuscarDespesaOutput>>>> BuscarPorPeriodo(int idUsuario, DateTime DataInicio, DateTime DataFim)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (idUsuario.ToString() != userRequestId)
            {
                return Forbid();
            }

            var despesas = await _buscarDespesa.BuscarPorPeriodo(idUsuario, DataInicio, DataFim);

            if (despesas == null)
            {
                return NotFound();
            }

            var resultado = new ResultadoAPI<List<BuscarDespesaOutput>>(StatusResult.Success, despesas);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResultadoAPI<EditarDespesaOutput>>> EditarDespesa(int id, EditarDespesaInput novaDespesa)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (!await _authenticationService.VerificaAutorizacaoDespesa(Int32.Parse(userRequestId), id))
            {
                return Forbid();
            }

            var despesa = await _editarDespesa.Editar(id, novaDespesa);

            if (despesa.Erros is not null)
            {
                return BadRequest(new ResultadoAPI<EditarDespesaOutput>(StatusResult.Error, null, despesa.Erros));
            }

            var resultado = new ResultadoAPI<EditarDespesaOutput>(StatusResult.Success, despesa.Valor);
            return Ok(resultado);
        }


        [HttpDelete()]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResultadoAPI<bool>>> ExcluirDespesa(int id)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (!await _authenticationService.VerificaAutorizacaoDespesa(Int32.Parse(userRequestId), id))
            {
                return Forbid();
            }

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
