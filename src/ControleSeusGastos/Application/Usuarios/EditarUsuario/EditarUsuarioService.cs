using Application.Usuarios.EditarUsuario.DTO;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Usuarios.EditarUsuario
{
    internal class EditarUsuarioService : IEditarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public EditarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<EditarUsuarioOutput> editar(EditarUsuarioInput usuario)
        {
            Usuario novoUsuario = new Usuario() { password = usuario.password, username = usuario.username};

            await _usuarioRepository.Atualizar(novoUsuario);

            EditarUsuarioOutput resul = new EditarUsuarioOutput()
            {
                username = usuario.username
            };

            return resul;

        }
    }
}
