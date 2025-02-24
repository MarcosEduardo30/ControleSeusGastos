namespace Application.Despesas.ExcluirDespesa
{
    public interface IExcluirDespesa
    {
        public Task<bool> Excluir(int despesaId);
    }
}
