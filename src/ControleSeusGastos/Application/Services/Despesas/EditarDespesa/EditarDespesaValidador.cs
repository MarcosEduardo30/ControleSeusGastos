using Application.Services.Despesas.EditarDespesa.DTO;
using Application.Validacao;

namespace Application.Services.Despesas.EditarDespesa
{
    public class EditarDespesaValidador : IValidador<EditarDespesaInput>
    {
        private readonly IValidadorDatabase _validadorDatabase;
        public EditarDespesaValidador(IValidadorDatabase validadorDatabase)
        {
            _validadorDatabase = validadorDatabase;
        }

        public async Task<List<Erro>> validar(EditarDespesaInput input, int? id)
        {
            List<Erro> erros = new List<Erro>();

            
            if(await _validadorDatabase.ExisteDespesaDB((int)id) == false)
            {
                erros.Add(new Erro("despesa_invalida", "Despesa não foi encontrada"));
                return erros;
            }

            if (input.Valor <= 0)
            {
                erros.Add(new Erro("Valor_Invalido", "Valor da despesa deve ser maior do que zero"));
                return erros;
            }

            if (input.Nome is null || input.Nome.Trim() == "")
            {
                erros.Add(new Erro("Nome_Vazio", "Campo nome não pode ser vazio"));
                return erros;
            }

            if (input.Nome.Trim().Length < 3)
            {
                erros.Add(new Erro("Nome_Curto", "Nome da despesa deve ser maior que 3 caracteres"));
                return erros;
            }

            if (input.Categoria_id is not null)
            {
                if (await _validadorDatabase.ExisteCategoriaDB((int)input.Categoria_id) == false)
                {
                    erros.Add(new Erro("Categoria_Invalida", "A Categoria da despesa não existe"));
                    return erros;
                }
            }

            return erros;
        }
    }
}
