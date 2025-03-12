using Application.Services.Usuarios.Login.DTO;

namespace Application.Services.Usuarios.Authentication
{
    public interface IAuthenticationService
    {
        public Task<string> Login(LoginInput input);
        public Task<bool> VerificaAutorizacaoDespesa(int UserRequestId, int DespesaId);
    }
}
