
using Application.Services.Usuarios.Authentication;
using Application.Services.Usuarios.BuscarUsuario;
using Application.Services.Usuarios.BuscarUsuario.DTO;
using Application.Services.Usuarios.CriarUsuario;
using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Services.Usuarios.EditarUsuario;
using Application.Services.Usuarios.EditarUsuario.DTO;
using Application.Services.Usuarios.ExcluirUsuario;
using Application.Services.Usuarios.Login.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ControleSeusGastos.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : Controller
    {
        private readonly ICriarUsuarioService _criarUsuarioService;
        private readonly IBuscarUsuarioService _buscarUsuarioService;
        private readonly IEditarUsuarioService _editarUsuarioService;
        private readonly IExcluirUsuarioService _excluirUsuarioService;
        private readonly IAuthenticationService _authenticationService;

        public UsuariosController(
            ICriarUsuarioService criarUsuarioService,
            IBuscarUsuarioService buscarUsuarioService,
            IEditarUsuarioService editarUsuarioService,
            IExcluirUsuarioService excluirUsuarioService,
            IAuthenticationService authenticationService)
        {
            _criarUsuarioService = criarUsuarioService;
            _buscarUsuarioService = buscarUsuarioService;
            _editarUsuarioService = editarUsuarioService;
            _excluirUsuarioService = excluirUsuarioService;
            _authenticationService = authenticationService;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BuscarUsuarioDTO>> BuscarUsuario(int id)
        {
            return await _buscarUsuarioService.buscar(id);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CriarUsuarioOutput>> CriarUsuario(CriarUsuarioInput novoUsuario)
        {
            return await _criarUsuarioService.Criar(novoUsuario);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EditarUsuarioOutput>> EditarUsuario(EditarUsuarioInput usuarioAtualizado)
        {
            return await _editarUsuarioService.editar(usuarioAtualizado);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> ExcluirUsuario(int id)
        {
            return await _excluirUsuarioService.excluir(id);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<string>> Login(LoginInput input)
        {
            var token = await _authenticationService.Login(input);
            if (token is null)
            {
                return BadRequest();
            }
            return Ok(token);
        }
    }
}
