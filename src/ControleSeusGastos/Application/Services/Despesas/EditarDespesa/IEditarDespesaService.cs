using Application.Services.Despesas.EditarDespesa.DTO;

namespace Application.Services.Despesas.EditarDespesa
{
    public interface IEditarDespesaService
    {
        public Task<EditarDespesaOutput?> Editar(EditarDespesaInput NovaDespesa);
    }
}
