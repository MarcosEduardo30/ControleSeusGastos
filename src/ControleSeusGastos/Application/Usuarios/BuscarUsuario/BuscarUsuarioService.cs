using Application.Usuarios.BuscarUsuario.DTO;
using Infrastructure.Repositories.Usuarios;

namespace Application.Usuarios.BuscarUsuario
{
    internal class BuscarUsuarioService : IBuscarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public BuscarUsuarioService(IUsuarioRepository usuarioRepository) { 
            _usuarioRepository = usuarioRepository;
        }
        public async Task<BuscarUsuarioDTO> buscar(int id)
        {
            var usuario = await _usuarioRepository.BuscarPorId(id);
            BuscarUsuarioDTO resul = new BuscarUsuarioDTO()
            {
                username = usuario.username
            };

            return resul;
        }
    }
}
