using Application.Services.Usuarios.CriarUsuario.DTO;

namespace Application.Services.Usuarios.CriarUsuario
{
    public interface ICriarUsuarioService
    {
        public Task<Resultado<CriarUsuarioOutput>> Criar(CriarUsuarioInput dados);
    }
}
