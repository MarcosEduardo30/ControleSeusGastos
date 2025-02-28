using Application.Usuarios.EditarUsuario.DTO;

namespace Application.Usuarios.EditarUsuario
{
    public interface IEditarUsuarioService
    {
        public Task<EditarUsuarioOutput> editar(EditarUsuarioInput usuario);
    }
}
