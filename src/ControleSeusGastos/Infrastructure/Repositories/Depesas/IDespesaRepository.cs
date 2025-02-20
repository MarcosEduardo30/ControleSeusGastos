using Domain.Despesas;

namespace Infrastructure.Repositories.Depesas
{
    public interface IDespesaRepository
    {
        public Task<int> criar(Despesa despesa);
        public Task<Despesa?> buscaPorId(int id);
        //public Task<List<Despesa?>> buscarPorIdUsuario(int idUsuario);
        public Task<int> atualizar(Despesa despesa);
        public Task<int> removerPorId(int id);

    }
}
