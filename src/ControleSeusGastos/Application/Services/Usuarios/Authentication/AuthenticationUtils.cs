using Domain.RefreshToken;
using Domain.Usuarios;
using Infrastructure.Repositories.RefreshTokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Usuarios.Authentication
{
    public class AuthenticationUtils
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationUtils(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public string CreateAuthToken(string nomeUsuario, int idUsuario)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            })
;
            var JwtIssuer = _configuration["JwtIssuer"];
            var JwtAudience = _configuration["JwtAudience"];

            string simKey = _configuration["SymmetricKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(simKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = JwtIssuer,
                Audience = JwtAudience,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = creds
            };

            var securityToken = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        public async Task<RefreshToken> CreateRefreshToken(int UsuarioId)
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                IdUsuario = UsuarioId,
                DataExpiracao = DateTime.UtcNow.AddDays(30)
            };

            await _refreshTokenRepository.Criar(refreshToken);

            return refreshToken;
        }

        public async Task<RefreshToken?> UpdateRefreshToken(Guid refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.BuscarPorId(refreshTokenId);
            if (refreshToken is null)
                return null;

            refreshToken.Token = Guid.NewGuid();
            refreshToken.DataExpiracao = DateTime.UtcNow.AddDays(30);

            await _refreshTokenRepository.AtualizarToken(refreshToken);

            return refreshToken;
        }
    }
}
