using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Validacao;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.CriarUsuario
{
    internal class CriarUsuarioValidador : IValidador<CriarUsuarioInput>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CriarUsuarioValidador(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<List<Erro>> validar(CriarUsuarioInput input, int? id = null)
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

            if (input.name is null || input.name == "")
            {
                erros.Add(new Erro("Nome_Nulo", "O campo nome não pode ser vazio"));
                return erros;
            }

            var usuarioEmail = _usuarioRepository.BuscarPorEmail(input.email);

            if (usuarioEmail is not null)
            {
                erros.Add(new Erro("Email_Ja_Cadastrado", "O email já está cadastrado"));
            }

            if (input.password is null || input.password == "")
            {
                erros.Add(new Erro("Senha_Nula", "A senha não pode ser vazia"));
                return erros;
            }

            if (!input.password.Any(c => char.IsNumber(c)))
            {
                erros.Add(new Erro("Senha_Sem_Numero", "A senha deve possuir pelo menos um numero"));
                return erros;
            }

            if (!input.password.Any(c => char.IsUpper(c)))
            {
                erros.Add(new Erro("Senha_Sem_Maiscula", "A senha deve possuir pelo menos uma letra maiscula"));
                return erros;
            }

            if (!input.password.Any(c => !char.IsLetterOrDigit(c)))
            {
                erros.Add(new Erro("Senha_Sem_Simbolo", "A senha deve possuir pelo menos um caractere especial"));
                return erros;
            }

            return erros;
        }
    }
}
