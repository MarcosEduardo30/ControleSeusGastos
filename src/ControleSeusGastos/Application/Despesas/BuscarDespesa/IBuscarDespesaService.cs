using Application.Despesas.BuscarDespesa.DTO;

namespace Application.Despesas.BuscarDespesa
{
    public interface IBuscarDespesaService
    {
        public Task<BuscarDespesaOutput?> BuscarPorId(int idDespesa);
        public Task<List<BuscarDespesaOutput>?> BuscarPorIdUsuario(int IdUsuario);
        public Task<List<BuscarDespesaOutput>?> BuscarPorPeriodo(BuscarPorPeriodoInput input);
    }
}
