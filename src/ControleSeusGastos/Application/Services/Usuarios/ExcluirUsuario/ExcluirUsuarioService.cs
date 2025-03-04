using Infrastructure.Repositories.Usuarios;

namespace Application.Services.Usuarios.ExcluirUsuario
{
    internal class ExcluirUsuarioService : IExcluirUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<bool> excluir(int id)
        {
            var afetadas = await _usuarioRepository.RemoverPorId(id);

            if (afetadas > 1)
            {
                return true;
            }
            return false;
        }
    }
}
