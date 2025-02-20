using Domain.Despesas;
using Infrastructure.Repositories.Depesas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.CriarDespesa
{
    public class CriarDespesa : ICriarDespesa
    {
        private readonly IDespesaRepository _despesaRepository;
        public CriarDespesa(IDespesaRepository despesaRepository) { 
            _despesaRepository = despesaRepository;
        }

        public async Task<CriarDespesaOutput> CriarNovaDespesa(CriarDespesaInput input)
        {
            var novaDespesa = new Despesa()
            {
                Nome = input.Nome,
                Descricao = input.Descricao,
                Data = input.Data
            };

            await _despesaRepository.criar(novaDespesa);

            var despesaOutput = new CriarDespesaOutput()
            {
                Nome = novaDespesa.Nome,
                Descricao = novaDespesa.Descricao,
                Data = novaDespesa.Data
            };

            return despesaOutput;
        }
    }
}
