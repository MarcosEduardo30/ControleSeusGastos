using Domain.RefreshToken;

namespace Infrastructure.Repositories.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        public Task<int> Criar(RefreshToken refreshToken);
        public Task<RefreshToken?> BuscarPorToken(Guid Token);
        public Task<RefreshToken?> BuscarPorId(Guid tokenId);
        public Task<int> AtualizarToken(RefreshToken refreshToken);
    }
}
