using Application.Despesas.CriarDespesa.DTO;

namespace Application.Despesas.CriarDespesa
{
    public interface ICriarDespesaService
    {
        public Task<CriarDespesaOutput> CriarNovaDespesa(CriarDespesaInput input);
    }
}
