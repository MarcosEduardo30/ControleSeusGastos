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

        public async Task<CriarUsuarioOutput> Criar(CriarUsuarioInput dados)
        {
            Usuario novoUsuario = new Usuario()
            {
                username = dados.username,
                password = dados.password
            };

            await _usuarioRepository.Criar(novoUsuario);

            CriarUsuarioOutput resul = new CriarUsuarioOutput()
            {
                username = novoUsuario.username
            };

            return resul;

        }
    }
}
