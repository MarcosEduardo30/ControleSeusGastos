using Application.Usuarios.BuscarUsuario.DTO;

namespace Application.Usuarios.BuscarUsuario
{
    public interface IBuscarUsuarioService
    {
        public Task<BuscarUsuarioDTO> buscar(int id);
    }
}
