using Application.Validacao;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.ExcluirDespesa
{
    internal class ExcluirDespesaService : IExcluirDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly IValidadorDatabase _validadorDatabase;

        public ExcluirDespesaService(IDespesaRepository despesaRepository, IValidadorDatabase validadorDatabase)
        {
            _despesaRepository = despesaRepository;
            _validadorDatabase = validadorDatabase;
        }

        public async Task<bool> Excluir(int despesaId)
        {
            if(await _validadorDatabase.ExisteDespesaDB(despesaId) == false)
            {
                return false;
            }

            var afetadas = await _despesaRepository.removerPorId(despesaId);
            if (afetadas > 1)
            {
                return true;
            }
            return false;

        }
    }
}
