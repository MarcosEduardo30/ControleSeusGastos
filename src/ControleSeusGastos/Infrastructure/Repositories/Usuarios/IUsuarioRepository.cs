using Domain.Usuarios;

namespace Infrastructure.Repositories.Usuarios
{
    public interface IUsuarioRepository
    {
        public Task<int> Criar(Usuario usuario);
        public Task<Usuario?> BuscarPorId(int id);
        public Task<Usuario?> BuscarPorUsername(string username);
        public Task<Usuario?> BuscarPorEmail(string email);
        public Task<int> Atualizar(Usuario usuario);
        public Task<int> RemoverPorId(int id);
    }
}
