namespace Application.Validacao
{
    public interface IValidadorDatabase
    {
        public Task<bool> ExisteDespesaDB(int despesaId);
        public Task<bool> ExisteUsuarioDB(int UsuarioID);
    }
}
