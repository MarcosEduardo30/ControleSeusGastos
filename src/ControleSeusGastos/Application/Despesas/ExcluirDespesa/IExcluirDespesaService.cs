namespace Application.Despesas.ExcluirDespesa
{
    public interface IExcluirDespesaService
    {
        public Task<bool> Excluir(int despesaId);
    }
}
