using Domain.Categorias;
using Domain.Usuarios;

namespace Domain.Despesas
{
    public class Despesa
    {
        public int Id { get; set; }
        public double Valor {  get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }   
        public int Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }
        public int Usuario_Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
    }
}
