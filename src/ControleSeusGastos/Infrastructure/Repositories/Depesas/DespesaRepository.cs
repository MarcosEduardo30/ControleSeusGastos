using Domain.Despesas;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Depesas
{
    internal class DespesaRepository : IDespesaRepository
    {
        internal readonly AppDbContext _dbContext;
        public DespesaRepository(AppDbContext dbContext) { 
            _dbContext = dbContext;
        }
        public async Task<int> criar(Despesa despesa)
        {
            _dbContext.Despesas.Add(despesa);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Despesa?> buscaPorId(int id)
        {
            return await _dbContext.Despesas
                .Include(d => d.Categoria)
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> atualizar(Despesa despesa)
        {
            _dbContext.Despesas.Update(despesa);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> removerPorId(int id)
        {
            return await _dbContext.Despesas.Where(d => d.Id == id).ExecuteDeleteAsync();   
        }

        public async Task<List<Despesa>?> buscarPorIdUsuario(int idUsuario)
        {
            return await _dbContext.Despesas
                .Where(d => d.Usuario_Id == idUsuario)
                .Include(d => d.Categoria)
                .Include(d => d.Usuario)
                .ToListAsync();
        }

        public async Task<List<Despesa>?> buscarPorPeriodo(int idUsuario, DateTime inicio, DateTime fim)
        {
            return await _dbContext.Despesas
                .Where(d => d.Usuario_Id == idUsuario && d.Data >= inicio.ToUniversalTime() && d.Data <= fim.ToUniversalTime())
                .Include(d => d.Categoria)
                .Include(d => d.Usuario)
                .ToListAsync();
        }
    }
}
