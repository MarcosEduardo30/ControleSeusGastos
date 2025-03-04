using Application.Services.Despesas.CriarDespesa.DTO;

namespace Application.Services.Despesas.CriarDespesa
{
    public interface ICriarDespesaService
    {
        public Task<CriarDespesaOutput> CriarNovaDespesa(CriarDespesaInput input);
    }
}
