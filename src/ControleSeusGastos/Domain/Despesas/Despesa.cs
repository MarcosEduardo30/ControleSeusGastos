using Domain.Enums;
using Domain.Usuarios;

namespace Domain.Despesas
{
    public class Despesa
    {
        public int Id { get; set; }
        public double Valor {  get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }   
        public CategoriaEnum Categoria { get; set; }
        public int Usuario_Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
    }
}
