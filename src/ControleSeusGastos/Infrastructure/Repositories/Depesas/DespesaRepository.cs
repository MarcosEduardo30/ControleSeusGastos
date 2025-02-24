using Domain.Despesas;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _dbContext.Despesas.FirstOrDefaultAsync(d => d.Id == id);
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
    }
}
