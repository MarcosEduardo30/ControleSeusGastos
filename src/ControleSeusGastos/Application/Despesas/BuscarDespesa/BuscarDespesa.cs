using Application.Despesas.BuscarDespesa.DTO;
using Infrastructure.Repositories.Depesas;

namespace Application.Despesas.BuscarDespesa
{
    internal class BuscarDespesa : IBuscarDespesa
    {
        private readonly IDespesaRepository _despesaRepository;
        public BuscarDespesa(IDespesaRepository despesaRepository) {
            _despesaRepository = despesaRepository;
        }

        public async Task<BuscarDespesaDTO?> Buscar(int id)
        {
            var despesa = await _despesaRepository.buscaPorId(id);

            if (despesa == null) { 
                return null;
            }

            var despesaDTO = new BuscarDespesaDTO
            {
                Data = despesa.Data,
                Descricao = despesa.Descricao,
                Nome = despesa.Nome
            };

            return despesaDTO;
        }
    }
}
