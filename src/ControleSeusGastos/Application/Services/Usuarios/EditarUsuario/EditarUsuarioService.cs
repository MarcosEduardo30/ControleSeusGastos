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

        public async Task<EditarUsuarioOutput> editar(EditarUsuarioInput NovoUsuario)
        {
            Usuario? usuario = await _usuarioRepository.BuscarPorId(NovoUsuario.id);

            if (usuario == null)
            {
                return null;
            }

            usuario.username = NovoUsuario.username;
            usuario.password = NovoUsuario.password;

            await _usuarioRepository.Atualizar(usuario);

            EditarUsuarioOutput resul = new EditarUsuarioOutput()
            {
                username = usuario.username
            };

            return resul;

        }
    }
}
