using Domain.Categorias;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Categorias
{
    internal class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoriaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Categoria?> buscarPorId(int id)
        {
            return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Categoria>> buscarCategorias()
        {
            return await _dbContext.Categorias.ToListAsync();
        }

    }
}
