namespace Application.Despesas.CriarDespesa
{
    public record CriarDespesaDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
