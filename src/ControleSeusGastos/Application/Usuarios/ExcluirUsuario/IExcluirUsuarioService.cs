namespace Application.Usuarios.ExcluirUsuario
{
    public interface IExcluirUsuarioService
    {
        public Task<bool> excluir(int id);
    }
}
