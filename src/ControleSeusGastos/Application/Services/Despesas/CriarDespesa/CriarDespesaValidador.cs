using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Validacao;
using Infrastructure.Repositories.Categorias;
using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Despesas.CriarDespesa
{
    internal class CriarDespesaValidador : IValidador<CriarDespesaInput>
    {
        private readonly IValidadorDatabase _validadorDB;
        public CriarDespesaValidador(IValidadorDatabase validadorDB)
        {
            _validadorDB = validadorDB;
        }

        public async Task<List<Erro>> validar(CriarDespesaInput input)
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

            if(input.Categoria_Id is not null)
            {
                if (await _validadorDB.ExisteCategoriaDB((int)input.Categoria_Id) == false)
                {
                    erros.Add(new Erro("Categoria_Invalida", "A Categoria da despesa não existe"));
                    return erros;
                }
            }

            if (await _validadorDB.ExisteUsuarioDB(input.Usuario_Id) == false)
            {
                erros.Add(new Erro("Usuario_Invalido", "Usuário da despesa não está cadastrado"));
                return erros;
            }

            return erros;
        }
    }
}
