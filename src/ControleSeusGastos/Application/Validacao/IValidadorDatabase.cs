namespace Application.Validacao
{
    public interface IValidadorDatabase
    {
        public Task<bool> ExisteCategoriaDB(int categoriaID);
        public Task<bool> ExisteDespesaDB(int despesaId);
        public Task<bool> ExisteUsuarioDB(int UsuarioID);
    }
}
