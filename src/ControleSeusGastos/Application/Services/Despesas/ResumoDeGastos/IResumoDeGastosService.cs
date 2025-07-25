using Application.Services.Despesas.ResumoDeGastos.DTO;

namespace Application.Services.Despesas.ResumoDeGastos
{
    public interface IResumoDeGastosService
    {
        Task<Resultado<ResumoDeGastosOutput>> BuscarResumo(int idUsuario);
    }
}
