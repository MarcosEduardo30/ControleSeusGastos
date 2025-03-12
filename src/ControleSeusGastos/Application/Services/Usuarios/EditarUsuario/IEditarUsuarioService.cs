using Application.Services.Usuarios.EditarUsuario.DTO;

namespace Application.Services.Usuarios.EditarUsuario
{
    public interface IEditarUsuarioService
    {
        public Task<Resultado<EditarUsuarioOutput>?> editar(int idUsuario, EditarUsuarioInput usuario);
    }
}
