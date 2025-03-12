using Application.Services.Usuarios.CriarUsuario.DTO;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.CriarUsuario
{
    internal class CriarUsuarioService : ICriarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Resultado<CriarUsuarioOutput>> Criar(CriarUsuarioInput dados)
        {
            Usuario novoUsuario = new()
            {
                name = dados.name,
                email = dados.email,
                username = dados.username
            };

            await _usuarioRepository.Criar(novoUsuario);

            CriarUsuarioOutput output = new()
            {
                name = novoUsuario.name,
                email = novoUsuario.email
            };

            Resultado<CriarUsuarioOutput> resul = new(null, output);

            return resul;

        }
    }
}
