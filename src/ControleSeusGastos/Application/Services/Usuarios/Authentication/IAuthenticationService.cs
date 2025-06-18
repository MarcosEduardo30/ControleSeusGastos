using Application.Services.Usuarios.Authentication.DTO;
using Application.Services.Usuarios.Login.DTO;

namespace Application.Services.Usuarios.Authentication
{
    public interface IAuthenticationService
    {
        public Task<loginOutput?> Login(LoginInput input);
        public Task<bool> VerificaAutorizacaoDespesa(int UserRequestId, int DespesaId);
    }
}
