using Application.Services.Despesas.ResumoDeGastos.DTO;
using Application.Validacao;
using Infrastructure.Repositories.Depesas;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.Despesas.ResumoDeGastos
{
    internal class ResumoDeGastosService : IResumoDeGastosService
    {
        private readonly IDespesaRepository _despesaRepository;
        public ResumoDeGastosService(IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }

        public async Task<Resultado<ResumoDeGastosOutput>> BuscarResumo(int idUsuario)
        {
            var despesas = await _despesaRepository.buscarPorIdUsuario(idUsuario);

            if (despesas == null)
            {
                var erro = new Erro("Despesas não encontradas", "Nenhuma despesa encontrada para esse usuário");
                return new Resultado<ResumoDeGastosOutput>([erro], null);
            }

            double gastoAnual = 0;
            despesas.FindAll(d => d.Data.Year == DateTime.Today.Year)
                    .ForEach(d => gastoAnual += d.Valor);

            double gastoMensal = 0;
            despesas.FindAll(d => d.Data.Year == DateTime.Today.Year && d.Data.Month == DateTime.Today.Month)
                    .ForEach(d => gastoMensal += d.Valor);

            var output = new ResumoDeGastosOutput() { 
                TotalGastoAno = gastoAnual,
                TotalGastoMes = gastoMensal
            };

            return new Resultado<ResumoDeGastosOutput>(null, output);
        }
    }
}
