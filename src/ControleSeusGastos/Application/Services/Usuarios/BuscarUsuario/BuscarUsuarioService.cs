using Application.Services.Usuarios.BuscarUsuario.DTO;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.BuscarUsuario
{
    internal class BuscarUsuarioService : IBuscarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public BuscarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<BuscarUsuarioDTO> buscar(int id)
        {
            var usuario = await _usuarioRepository.BuscarPorId(id);

            if (usuario == null)
                return null;

            BuscarUsuarioDTO resul = new BuscarUsuarioDTO()
            {
                username = usuario.username,
                name = usuario.name,
                email = usuario.email
            };

            return resul;
        }
    }
}
