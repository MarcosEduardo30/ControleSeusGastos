using Application.Despesas.CriarDespesa.DTO;

namespace Application.Despesas.CriarDespesa
{
    public interface ICriarDespesaService
    {
        public Task<CriarDespesaDTO> CriarNovaDespesa(CriarDespesaDTO input);
    }
}
