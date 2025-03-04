using Application.Services.Despesas.EditarDespesa.DTO;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.EditarDespesa
{
    internal class EditarDespesaService : IEditarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;

        public EditarDespesaService(IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }
        public async Task<EditarDespesaOutput?> Editar(EditarDespesaInput NovaDespesa)
        {
            var despesaAntiga = await _despesaRepository.buscaPorId(NovaDespesa.Id);
            if (despesaAntiga == null)
            {
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
