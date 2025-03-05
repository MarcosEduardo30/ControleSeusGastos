namespace Application.Validacao
{
    internal interface IValidador<T>
    {
        public Task<List<Erro>> validar(T input, int? id = null);
    }
}
