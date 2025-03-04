namespace Application.Services.Usuarios.ExcluirUsuario
{
    public interface IExcluirUsuarioService
    {
        public Task<bool> excluir(int id);
    }
}
