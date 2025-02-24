using Application.Despesas.EditarDespesa.DTO;
using Infrastructure.Repositories.Depesas;

namespace Application.Despesas.EditarDespesa
{
    internal class EditarDespesa : IEditarDespesa
    {
        private readonly IDespesaRepository _despesaRepository;

        public EditarDespesa(IDespesaRepository despesaRepository) { 
            _despesaRepository = despesaRepository;
        }
        public async Task<EditarDespesaOutput?> Editar(EditarDespesaInput NovaDespesa)
        {
            var despesaAntiga = await _despesaRepository.buscaPorId(NovaDespesa.Id);
            if (despesaAntiga == null) {
                return null;
            }

            despesaAntiga.Nome = NovaDespesa.Nome;
            despesaAntiga.Descricao = NovaDespesa.Descricao;
            despesaAntiga.Data = NovaDespesa.Data;

            await _despesaRepository.atualizar(despesaAntiga);

            var resul = new EditarDespesaOutput
            {
                Nome = despesaAntiga.Nome,
                Descricao = despesaAntiga.Descricao,
                Data = despesaAntiga.Data,
            };

            return resul;
        }
    }
}
