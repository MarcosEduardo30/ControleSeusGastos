namespace Application.Validacao
{
    internal interface IValidador<T>
    {
        public List<Erro> validar(T input);
    }
}
