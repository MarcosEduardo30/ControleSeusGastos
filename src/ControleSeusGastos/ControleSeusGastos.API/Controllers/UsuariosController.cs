
using Application.Services.Usuarios.Authentication;
using Application.Services.Usuarios.Authentication.DTO;
using Application.Services.Usuarios.Authentication.LoginRefreshToken;
using Application.Services.Usuarios.BuscarUsuario;
using Application.Services.Usuarios.BuscarUsuario.DTO;
using Application.Services.Usuarios.CriarUsuario;
using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Services.Usuarios.EditarUsuario;
using Application.Services.Usuarios.EditarUsuario.DTO;
using Application.Services.Usuarios.ExcluirUsuario;
using Application.Services.Usuarios.Login.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private readonly ILoginRefreshTokenService _loginRefreshTokenService;

        public UsuariosController(
            ICriarUsuarioService criarUsuarioService,
            IBuscarUsuarioService buscarUsuarioService,
            IEditarUsuarioService editarUsuarioService,
            IExcluirUsuarioService excluirUsuarioService,
            IAuthenticationService authenticationService,
            ILoginRefreshTokenService loginRefreshTokenService)
        {
            _criarUsuarioService = criarUsuarioService;
            _buscarUsuarioService = buscarUsuarioService;
            _editarUsuarioService = editarUsuarioService;
            _excluirUsuarioService = excluirUsuarioService;
            _authenticationService = authenticationService;
            _loginRefreshTokenService = loginRefreshTokenService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BuscarUsuarioDTO>> BuscarUsuario(int id)
        {
            var usuario = await _buscarUsuarioService.buscar(id);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CriarUsuarioOutput>> CriarUsuario(CriarUsuarioInput novoUsuario)
        {
            var usuario = await _criarUsuarioService.Criar(novoUsuario);
            if (usuario.Erros is not null)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EditarUsuarioOutput>> EditarUsuario(int id, EditarUsuarioInput usuarioAtualizado)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (id.ToString() != userRequestId)
            {
                return Forbid();
            }

            var usuario = await _editarUsuarioService.editar(id, usuarioAtualizado);
            if (usuario is null)
                return NotFound();
            if (usuario.Erros is not null)
                return BadRequest(usuario);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> ExcluirUsuario(int id)
        {
            string? userRequestId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (id.ToString() != userRequestId)
            {
                return Forbid();
            }
            await _excluirUsuarioService.excluir(id);

            return NoContent();
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<loginOutput?>> Login(LoginInput input)
        {
            var token = await _authenticationService.Login(input);
            if (token is null)
            {
                return BadRequest();
            }
            return Ok(token);
        }

        [HttpPost("LoginRefreshToken")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<loginOutput?>> LoginRefreshToken([FromBody] Guid RefreshToken)
        {
            var response = await _loginRefreshTokenService.Login(RefreshToken);

            if (response is null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
