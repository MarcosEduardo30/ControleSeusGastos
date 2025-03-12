using Application.Services.Usuarios.EditarUsuario.DTO;
using Application.Validacao;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.EditarUsuario
{
    internal class EditarUsuarioValidador : IValidador<EditarUsuarioInput>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public EditarUsuarioValidador(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Erro>> validar(EditarUsuarioInput input, int? id = null)
        {
            List<Erro> erros = [];

            if (input.username is null || input.username == "")
            {
                erros.Add(new Erro("Usuario_Nulo", "Nome de usuario não pode ser vazio"));
                return erros;
            }

            if (input.username.Length < 3)
            {
                erros.Add(new Erro("Usuario_Muito_Curto", "Nome de usuario precisa ter mais de 3 caracteres"));
                return erros;
            }

            var usuarioUsername = await _usuarioRepository.BuscarPorUsername(input.username);
            if (usuarioUsername is not null)
            {
                erros.Add(new Erro("Usuario_Ja_Cadastrado", "O nome de usuario já está em uso"));
                return erros;
            }

            if (input.name is null || input.name == "")
            {
                erros.Add(new Erro("Nome_Nulo", "O campo nome não pode ser vazio"));
                return erros;
            }

            var usuarioEmail = await _usuarioRepository.BuscarPorEmail(input.email);

            if (usuarioEmail is not null)
            {
                erros.Add(new Erro("Email_Ja_Cadastrado", "O email já está cadastrado"));
                return erros;
            }

            return erros;
        }
    }
}
