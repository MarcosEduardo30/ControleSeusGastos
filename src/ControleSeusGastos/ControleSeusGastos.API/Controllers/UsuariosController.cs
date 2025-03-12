﻿
using Application.Services.Usuarios.Authentication;
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


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BuscarUsuarioDTO>> BuscarUsuario(int id)
        {
            var usuario = await _buscarUsuarioService.buscar(id);
            if (usuario is null)
                return NotFound();

            return usuario;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CriarUsuarioOutput>> CriarUsuario(CriarUsuarioInput novoUsuario)
        {
            return await _criarUsuarioService.Criar(novoUsuario);
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

            return await _editarUsuarioService.editar(usuarioAtualizado);
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
