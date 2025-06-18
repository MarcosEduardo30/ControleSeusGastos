using Application.Services.Usuarios.Authentication.DTO;

namespace Application.Services.Usuarios.Authentication.LoginRefreshToken
{
    public interface ILoginRefreshTokenService
    {
        public Task<loginOutput> Login(Guid RefreshToken);
    }
}
