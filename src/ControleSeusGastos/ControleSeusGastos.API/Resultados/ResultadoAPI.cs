using Application.Validacao;

namespace ControleSeusGastos.API.Resultados
{
    public class ResultadoAPI<T>
    {
        public StatusResult status { get; set; }
        public T? data { get; set; }
        public List<Erro>? erros { get; set; }

        public ResultadoAPI(StatusResult Status, T? Data, List<Erro>? Erros = null)
        {
            status = Status;
            data = Data;
            erros = Erros;
        }
    }
}
