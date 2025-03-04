using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Validacao;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Despesas.CriarDespesa
{
    internal class CriarDespesaValidador : IValidador<CriarDespesaInput>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarDespesaValidador(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<Erro> validar(CriarDespesaInput input)
        {
            List<Erro> erros = new List<Erro>();

            if (input.Valor < 0)
            {
                erros.Add(new Erro("Valor_Invalido", "Valor da despesa deve ser maior do que zero"));
                return erros;
            }

            if (input.Nome == "" || input.Nome is null)
            {
                erros.Add(new Erro("Nome_Vazio", "Campo nome não pode ser vazio"));
                return erros;
            }

            if (input.Nome.Length < 3)
            {
                erros.Add(new Erro("Nome_Curto", "Nome da despesa deve ser maior que 3 caracteres"));
                return erros;
            }

            // Validações da categoria serão feitas depois

            if (!UsuarioExisteDB(input.Usuario_Id))
            {
                erros.Add(new Erro("Usuario_Invalido", "Usuário da despesa não está cadastrado"));
                return erros;
            }

            return erros;
        }

        private bool UsuarioExisteDB(int usuarioId)
        {
            var usuario = _usuarioRepository.BuscarPorId(usuarioId);
            if (usuario is null)
            {
                return false;
            }
            return true;
        }
    }
}
