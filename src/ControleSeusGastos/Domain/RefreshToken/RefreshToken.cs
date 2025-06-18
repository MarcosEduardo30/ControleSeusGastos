using Domain.Usuarios;

namespace Domain.RefreshToken
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}
