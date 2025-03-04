using Application.Services.Usuarios.EditarUsuario.DTO;

namespace Application.Services.Usuarios.EditarUsuario
{
    public interface IEditarUsuarioService
    {
        public Task<EditarUsuarioOutput> editar(EditarUsuarioInput usuario);
    }
}
