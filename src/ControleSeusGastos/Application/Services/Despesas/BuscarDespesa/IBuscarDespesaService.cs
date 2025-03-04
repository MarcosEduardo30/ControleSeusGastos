using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.BuscarDespesa.DTO;

namespace Application.Services.Despesas.BuscarDespesa
{
    public interface IBuscarDespesaService
    {
        public Task<BuscarDespesaOutput?> BuscarPorId(int idDespesa);
        public Task<List<BuscarDespesaOutput>?> BuscarPorIdUsuario(int IdUsuario);
        public Task<List<BuscarDespesaOutput>?> BuscarPorPeriodo(BuscarPorPeriodoInput input);
    }
}
