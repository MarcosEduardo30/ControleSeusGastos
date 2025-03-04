using Application.Services.Usuarios.BuscarUsuario.DTO;

namespace Application.Services.Usuarios.BuscarUsuario
{
    public interface IBuscarUsuarioService
    {
        public Task<BuscarUsuarioDTO> buscar(int id);
    }
}
