using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.ExcluirDespesa
{
    internal class ExcluirDespesaService : IExcluirDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;

        public ExcluirDespesaService(IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }

        public async Task<bool> Excluir(int despesaId)
        {
            var afetadas = await _despesaRepository.removerPorId(despesaId);
            if (afetadas > 1)
            {
                return true;
            }
            return false;

        }
    }
}
