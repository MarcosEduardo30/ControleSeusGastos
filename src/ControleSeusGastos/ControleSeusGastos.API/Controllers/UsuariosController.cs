
using Application.Usuarios.BuscarUsuario;
using Application.Usuarios.BuscarUsuario.DTO;
using Application.Usuarios.CriarUsuario;
using Application.Usuarios.CriarUsuario.DTO;
using Application.Usuarios.EditarUsuario;
using Application.Usuarios.EditarUsuario.DTO;
using Application.Usuarios.ExcluirUsuario;
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

        public UsuariosController(
            ICriarUsuarioService criarUsuarioService,
            IBuscarUsuarioService buscarUsuarioService,
            IEditarUsuarioService editarUsuarioService,
            IExcluirUsuarioService excluirUsuarioService)
        {
            _criarUsuarioService = criarUsuarioService;
            _buscarUsuarioService = buscarUsuarioService;
            _editarUsuarioService = editarUsuarioService;
            _excluirUsuarioService = excluirUsuarioService;
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
    }
}
