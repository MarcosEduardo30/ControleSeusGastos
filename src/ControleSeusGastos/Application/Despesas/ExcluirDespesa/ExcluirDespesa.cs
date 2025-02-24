using Infrastructure.Repositories.Depesas;

namespace Application.Despesas.ExcluirDespesa
{
    internal class ExcluirDespesa : IExcluirDespesa
    {
        private readonly IDespesaRepository _despesaRepository;

        public ExcluirDespesa(IDespesaRepository despesaRepository)
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
