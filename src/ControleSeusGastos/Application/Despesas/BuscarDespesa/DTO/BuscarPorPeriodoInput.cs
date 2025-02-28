
namespace Application.Despesas.BuscarDespesa.DTO
{
    public class BuscarPorPeriodoInput
    {
        public int idUsuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set;}
    }
}
