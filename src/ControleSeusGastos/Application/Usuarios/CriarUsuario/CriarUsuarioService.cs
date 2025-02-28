using Application.Usuarios.CriarUsuario.DTO;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Usuarios.CriarUsuario
{
    internal class CriarUsuarioService : ICriarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<EditarUsuarioOutput> Criar(CriarUsuarioInput dados)
        {
            Usuario novoUsuario = new Usuario()
            {
                username = dados.username,
                password = dados.password
            };

            await _usuarioRepository.Criar(novoUsuario);

            EditarUsuarioOutput resul = new EditarUsuarioOutput()
            {
                username = novoUsuario.username
            };

            return resul;

        }
    }
}
