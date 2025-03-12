namespace Application.Services.Usuarios.Login.DTO
{
    public record LoginInput
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
