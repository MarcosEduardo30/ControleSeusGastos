using Domain.Despesas;
using Domain.RefreshToken;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RefreshTokens
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Criar(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Add(refreshToken);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 1;
        }
    }
}
