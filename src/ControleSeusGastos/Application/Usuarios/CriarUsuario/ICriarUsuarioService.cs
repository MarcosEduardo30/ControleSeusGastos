using Application.Usuarios.CriarUsuario.DTO;

namespace Application.Usuarios.CriarUsuario
{
    public interface ICriarUsuarioService
    {
        public Task<CriarUsuarioOutput> Criar(CriarUsuarioInput dados);
    }
}
