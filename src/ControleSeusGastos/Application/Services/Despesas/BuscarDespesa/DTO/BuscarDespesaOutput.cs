using Domain.Enums;

namespace Application.Despesas.BuscarDespesa.DTO
{
    public record BuscarDespesaOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Categoria { get; set; }

        public string Usuario_Nome { get; set; }
    }
}
