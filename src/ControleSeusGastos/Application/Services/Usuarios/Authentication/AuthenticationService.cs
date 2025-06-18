using Application.Services.Usuarios.Authentication.DTO;
using Application.Services.Usuarios.Login.DTO;
using Domain.RefreshToken;
using Domain.Usuarios;
using Infrastructure.Repositories.Depesas;
using Infrastructure.Repositories.RefreshTokens;
using Infrastructure.Repositories.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Usuarios.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly IDespesaRepository _despesaRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            IUsuarioRepository usuarioRepository,
            IDespesaRepository despesaRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration)
        {
            _usuariosRepository = usuarioRepository;
            _despesaRepository = despesaRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<loginOutput?> Login(LoginInput input)
        {
            var usuario = await _usuariosRepository.BuscarPorUsername(input.username);
            
            if (usuario == null)
                return null;

            if (input.password != usuario.password)
                return null;

            var authToken = CreateToken(usuario.name, usuario.Id);

            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid(),
                IdUsuario = usuario.Id,
                DataExpiracao = DateTime.UtcNow.AddDays(30)
            };

            await _refreshTokenRepository.Criar(refreshToken);
            
            return new loginOutput() { token = authToken, RefreshToken = Guid.NewGuid(), UsuarioId = usuario.Id};
        }

        public async Task<bool> VerificaAutorizacaoDespesa(int UserRequestId, int DespesaId)
        {
            var despesa = await _despesaRepository.buscaPorId(DespesaId);

            if (despesa is null)
                return true;

            if (despesa.Usuario_Id != UserRequestId)
                return false;

            return true;

        }

        private string CreateToken(string nomeUsuario, int idUsuario)
        {
            var claims = new ClaimsIdentity(new []
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
    }
}
