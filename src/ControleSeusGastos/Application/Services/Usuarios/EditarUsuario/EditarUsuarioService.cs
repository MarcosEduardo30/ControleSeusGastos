using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Services.Usuarios.EditarUsuario.DTO;
using Application.Validacao;
using Domain.Usuarios;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.EditarUsuario
{
    internal class EditarUsuarioService : IEditarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly EditarUsuarioValidador _validador;

        public EditarUsuarioService(IUsuarioRepository usuarioRepository, EditarUsuarioValidador validador)
        {
            _usuarioRepository = usuarioRepository;
            _validador = validador;
        }

        public async Task<Resultado<EditarUsuarioOutput>?> editar(int idUsuario, EditarUsuarioInput NovoUsuario)
        {
            List<Erro> erros = await _validador.validar(NovoUsuario);

            if (erros.Count > 0)
            {
                Resultado<EditarUsuarioOutput> resulErro = new(erros, null);
                return resulErro;
            }

            Usuario? usuario = await _usuarioRepository.BuscarPorId(idUsuario);

            if (usuario == null)
            {
                return null;
            }

            usuario.name = NovoUsuario.name;
            usuario.email = NovoUsuario.email;
            usuario.username = NovoUsuario.username;

            await _usuarioRepository.Atualizar(usuario);

            EditarUsuarioOutput output = new EditarUsuarioOutput()
            {
                name = usuario.name,
                email = usuario.email,
                username = usuario.username
            };

            Resultado<EditarUsuarioOutput> resul = new(null, output);

            return resul;
        }
    }
}
