namespace Application.Services.Usuarios.Authentication.DTO
{
    public class loginOutput
    {
        public string token {  get; set; }
        public Guid RefreshToken { get; set; }
        public int UsuarioId { get; set; }
    }
}
