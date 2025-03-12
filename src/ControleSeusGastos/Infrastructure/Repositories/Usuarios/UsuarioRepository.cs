using Domain.Usuarios;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Usuarios
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _appDbContext;
        public UsuarioRepository(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        }

        public async Task<int> Atualizar(Usuario usuario)
        {
            var usuarioAntigo = _appDbContext.Usuarios.Update(usuario);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> BuscarPorUsername(string username)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.username == username);
        }

        public async Task<int> Criar(Usuario usuario)
        {
            await _appDbContext.Usuarios.AddAsync(usuario);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> RemoverPorId(int id)
        {
            return await _appDbContext.Usuarios.Where(u => u.Id == id).ExecuteDeleteAsync();
        }
    }
}
