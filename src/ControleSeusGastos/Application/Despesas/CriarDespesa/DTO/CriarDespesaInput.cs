namespace Application.Despesas.CriarDespesa.DTO
{
    public record CriarDespesaInput
    {
        public double Valor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Categoria_Id { get; set; }
        public int Usuario_Id { get; set; }
        public DateTime Data { get; set; }
    }
}
