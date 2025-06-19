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
        private readonly AuthenticationUtils _authenticationUtils;
        

        public AuthenticationService(
            IUsuarioRepository usuarioRepository,
            IDespesaRepository despesaRepository,
            AuthenticationUtils authenticationUtils)
        {
            _usuariosRepository = usuarioRepository;
            _despesaRepository = despesaRepository;
            _authenticationUtils = authenticationUtils;
        }

        public async Task<loginOutput?> Login(LoginInput input)
        {
            var usuario = await _usuariosRepository.BuscarPorUsername(input.username);
            
            if (usuario == null)
                return null;

            if (input.password != usuario.password)
                return null;

            var authToken = _authenticationUtils.CreateAuthToken(usuario.name, usuario.Id);

            RefreshToken refreshToken = await _authenticationUtils.CreateRefreshToken(usuario.Id);


            return new loginOutput() { token = authToken, RefreshToken = refreshToken.Token, UsuarioId = usuario.Id};
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
    }
}
