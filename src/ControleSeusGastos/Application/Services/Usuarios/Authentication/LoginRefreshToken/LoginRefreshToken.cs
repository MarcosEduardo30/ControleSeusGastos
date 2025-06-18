using Application.Services.Usuarios.Authentication.DTO;
using Infrastructure.Repositories.RefreshTokens;

namespace Application.Services.Usuarios.Authentication.LoginRefreshToken
{
    internal class LoginRefreshToken : ILoginRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuthenticationUtils _authenticationUtils;

        public LoginRefreshToken(IRefreshTokenRepository refreshTokenRepository, AuthenticationUtils authenticationUtils)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _authenticationUtils = authenticationUtils;
        }

        public async Task<loginOutput?> Login(Guid RefreshToken)
        {
            var token = await _refreshTokenRepository.BuscarPorToken(RefreshToken);

            if (token is null || token.DataExpiracao < DateTime.UtcNow)
            {
                return null;
            }

            var authtoken = _authenticationUtils.CreateAuthToken(token.Usuario.name, token.IdUsuario);
            var refreshToken = await _authenticationUtils.UpdateRefreshToken(token.Id);

            if (refreshToken is null)
                return null;

            return new loginOutput() { token = authtoken, RefreshToken = refreshToken.Token, UsuarioId = refreshToken.Usuario.Id};
        }
    }
}
