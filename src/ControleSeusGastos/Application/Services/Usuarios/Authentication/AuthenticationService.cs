using Application.Services.Usuarios.Login.DTO;
using Domain.Usuarios;
using Infrastructure.Repositories.Depesas;
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
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUsuarioRepository usuarioRepository, IDespesaRepository despesaRepository, IConfiguration configuration)
        {
            _usuariosRepository = usuarioRepository;
            _despesaRepository = despesaRepository;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginInput input)
        {
            var usuario = await _usuariosRepository.BuscarPorUsername(input.username);
            
            if (usuario == null)
                return null;

            if (input.password != usuario.password)
                return null;

            var token = CreateToken(usuario.name, usuario.Id);

            return token;
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

        public string CreateToken(string nomeUsuario, int idUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString())
            };


            string simKey = _configuration["SymmetricKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(simKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var JwtIssuer = _configuration["JwtIssuer"];
            var JwtAudience = _configuration["JwtAudience"];
            var tokenDescriptor = new JwtSecurityToken(
                issuer: JwtIssuer,
                audience: JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
