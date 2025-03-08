using Infrastructure.Repositories.Categorias;
using Infrastructure.Repositories.Depesas;
using Infrastructure.Repositories.Usuarios;

namespace Application.Validacao
{
    public class ValidadorDatabase : IValidadorDatabase
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ValidadorDatabase(IUsuarioRepository usuarioRepository, ICategoriaRepository categoriaRepository, IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> ExisteCategoriaDB(int categoriaID)
        {
            var categoria = await _categoriaRepository.buscarPorId(categoriaID);
            if (categoria is null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ExisteDespesaDB(int despesaId)
        {
            var despesa = await _despesaRepository.buscaPorId(despesaId);
            if (despesa is null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ExisteUsuarioDB(int UsuarioID)
        {
            var usuario = await _usuarioRepository.BuscarPorId(UsuarioID);
            if (usuario is null)
            {
                return false;
            }
            return true;
        }
    }
}
