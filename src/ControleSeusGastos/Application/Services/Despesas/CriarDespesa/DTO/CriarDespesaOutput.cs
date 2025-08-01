using Domain.Enums;

namespace Application.Services.Despesas.CriarDespesa.DTO
{
    public record CriarDespesaOutput
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
    }
}
