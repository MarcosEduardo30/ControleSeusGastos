using Application.Services.Usuarios.EditarUsuario.DTO;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.EditarUsuario
{
    internal class EditarUsuarioService : IEditarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public EditarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<EditarUsuarioOutput?> editar(int idUsuario, EditarUsuarioInput NovoUsuario)
        {
            Usuario? usuario = await _usuarioRepository.BuscarPorId(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            usuario.name = NovoUsuario.name;
            usuario.email = NovoUsuario.email;
            usuario.username = NovoUsuario.username;

            await _usuarioRepository.Atualizar(usuario);

            EditarUsuarioOutput resul = new EditarUsuarioOutput()
            {
                name = usuario.name,
                email = usuario.email,
                username = usuario.username
            };

            return resul;
        }
    }
}
