using Domain.Enums;

namespace Application.Services.Despesas.CriarDespesa.DTO
{
    public record CriarDespesaInput
    {
        public double Valor { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public int Usuario_Id { get; set; }
        public DateTime Data { get; set; }
    }
}
