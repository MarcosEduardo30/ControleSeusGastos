using Domain.RefreshToken;

namespace Infrastructure.Repositories.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        public Task<int> Criar(RefreshToken refreshToken);
    }
}
