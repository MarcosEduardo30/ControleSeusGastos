using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Validacao;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.CriarUsuario
{
    internal class CriarUsuarioService : ICriarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly CriarUsuarioValidador _validador;

        public CriarUsuarioService(IUsuarioRepository usuarioRepository, CriarUsuarioValidador validador)
        {
            _usuarioRepository = usuarioRepository;
            _validador = validador;
        }

        public async Task<Resultado<CriarUsuarioOutput>> Criar(CriarUsuarioInput dados)
        {
            List<Erro> erros = await _validador.validar(dados);

            if (erros.Count > 0) {
                Resultado<CriarUsuarioOutput> resulErro = new(erros, null);
                return resulErro;
            }

            Usuario novoUsuario = new()
            {
                name = dados.name,
                email = dados.email,
                password = dados.password,
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
