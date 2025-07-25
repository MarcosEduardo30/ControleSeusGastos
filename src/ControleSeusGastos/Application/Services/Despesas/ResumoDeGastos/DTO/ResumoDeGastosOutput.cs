namespace Application.Services.Despesas.ResumoDeGastos.DTO
{
    public record ResumoDeGastosOutput
    {
        public double TotalGastoMes {  get; set; }
        public double TotalGastoAno { get; set; }
        //public List<GastoPorCategoria> GastosPorCategoria { get; set; }
    }

    public record GastoPorCategoria
    {
        public string Categoria { get; set; }
        public double Valor { get; set; }
    }
}
