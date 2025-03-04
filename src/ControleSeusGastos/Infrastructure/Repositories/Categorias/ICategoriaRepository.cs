using Domain.Categorias;

namespace Infrastructure.Repositories.Categorias
{
    public interface ICategoriaRepository
    {
        public Task<Categoria?> buscarPorId(int id);
        public Task<List<Categoria>> buscarCategorias();
    }
}
