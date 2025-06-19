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

        public async Task<int> AtualizarToken(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Update(refreshToken);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken?> BuscarPorId(Guid tokenId)
        {
            return await _dbContext.RefreshTokens
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Id == tokenId);
        }

        public async Task<RefreshToken?> BuscarPorToken(Guid Token)
        {
            var teste = _dbContext.RefreshTokens.ToList();

            return await _dbContext.RefreshTokens
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Token == Token);
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
