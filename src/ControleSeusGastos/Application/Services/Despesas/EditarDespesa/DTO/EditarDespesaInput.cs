using Domain.Enums;

namespace Application.Services.Despesas.EditarDespesa.DTO
{
    public class EditarDespesaInput
    {
        public int Valor {  get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public CategoriaEnum Categoria { get; set; }
        public DateTime Data { get; set; }
    }
}
