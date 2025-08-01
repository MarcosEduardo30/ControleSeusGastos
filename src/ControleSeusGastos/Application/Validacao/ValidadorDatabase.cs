using Infrastructure.Repositories.Depesas;
using Infrastructure.Repositories.Usuarios;

namespace Application.Validacao
{
    public class ValidadorDatabase : IValidadorDatabase
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ValidadorDatabase(IUsuarioRepository usuarioRepository, IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
            _usuarioRepository = usuarioRepository;
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
