namespace ControleSeusGastos.API.Resultados
{
    public class ResultadoAPI<T>
    {
        public StatusResult status { get; set; }
        public T data { get; set; }

        public ResultadoAPI(StatusResult Status, T Data)
        {
            this.status = Status;
            this.data = Data;
        }
    }
}
