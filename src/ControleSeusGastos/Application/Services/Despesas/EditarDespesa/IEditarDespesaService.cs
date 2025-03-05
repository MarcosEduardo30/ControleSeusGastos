using Application.Services.Despesas.EditarDespesa.DTO;

namespace Application.Services.Despesas.EditarDespesa
{
    public interface IEditarDespesaService
    {
        public Task<Resultado<EditarDespesaOutput>> Editar(int id, EditarDespesaInput NovaDespesa);
    }
}
