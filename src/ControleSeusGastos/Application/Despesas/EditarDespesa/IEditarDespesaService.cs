using Application.Despesas.EditarDespesa.DTO;

namespace Application.Despesas.EditarDespesa
{
    public interface IEditarDespesaService
    {
        public Task<EditarDespesaOutput?> Editar(EditarDespesaInput NovaDespesa);
    }
}
