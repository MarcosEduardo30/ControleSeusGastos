using Domain.Despesas;

namespace Infrastructure.Repositories.Depesas
{
    public interface IDespesaRepository
    {
        public Task<int> criar(Despesa despesa);
        public Task<Despesa?> buscaPorId(int id);
        public Task<List<Despesa>?> buscarPorIdUsuario(int idUsuario);
        public Task<List<Despesa>?> buscarPorPeriodo(int idUsuario, DateTime inicio, DateTime fim);
        public Task<int> atualizar(Despesa despesa);
        public Task<int> removerPorId(int id);

    }
}
