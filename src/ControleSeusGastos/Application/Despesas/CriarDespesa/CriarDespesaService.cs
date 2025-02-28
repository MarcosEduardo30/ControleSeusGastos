using Application.Despesas.CriarDespesa.DTO;
using Domain.Despesas;
using Infrastructure.Repositories.Depesas;

namespace Application.Despesas.CriarDespesa
{
    internal class CriarDespesaService : ICriarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        public CriarDespesaService(IDespesaRepository despesaRepository) { 
            _despesaRepository = despesaRepository;
        }

        public async Task<CriarDespesaOutput> CriarNovaDespesa(CriarDespesaInput input)
        {
            var novaDespesa = new Despesa()
            {
                Nome = input.Nome,
                Valor = input.Valor,
                Descricao = input.Descricao,
                Data = input.Data,
                Categoria_Id = input.Categoria_Id,
                Usuario_Id = input.Usuario_Id
            };

            await _despesaRepository.criar(novaDespesa);
            

            var despesaOutput = new CriarDespesaOutput()
            {
                Nome = novaDespesa.Nome,
                Valor = novaDespesa.Valor,
                Descricao = novaDespesa.Descricao,
                //Categoria_Nome = novaDespesa.Categoria.nome,
                Data = novaDespesa.Data
            };

            return despesaOutput;
        }
    }
}
