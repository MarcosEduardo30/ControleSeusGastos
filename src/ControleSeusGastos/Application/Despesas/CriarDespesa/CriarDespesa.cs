using Application.Despesas.CriarDespesa.DTO;
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

        public async Task<CriarDespesaDTO> CriarNovaDespesa(CriarDespesaDTO input)
        {
            var novaDespesa = new Despesa()
            {
                Nome = input.Nome,
                Descricao = input.Descricao,
                Data = input.Data
            };

            await _despesaRepository.criar(novaDespesa);

            var despesaOutput = new CriarDespesaDTO()
            {
                Nome = novaDespesa.Nome,
                Descricao = novaDespesa.Descricao,
                Data = novaDespesa.Data
            };

            return despesaOutput;
        }
    }
}
