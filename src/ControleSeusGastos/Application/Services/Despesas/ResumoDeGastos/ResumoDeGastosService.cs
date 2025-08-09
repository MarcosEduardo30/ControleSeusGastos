using Application.Services.Despesas.ResumoDeGastos.DTO;
using Application.Validacao;
using Domain.Despesas;
using Domain.Enums;
using Infrastructure.Repositories.Depesas;
using System.Diagnostics.Metrics;
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

            
            var despesasDoAno = despesas.FindAll(d => d.Data.Year == DateTime.Today.Year);

            double gastoAnual = 0;
            despesasDoAno.ForEach(d => gastoAnual += d.Valor);

            double gastoMensal = 0;
            despesasDoAno.FindAll(d => d.Data.Month == DateTime.Today.Month)
                    .ForEach(d => gastoMensal += d.Valor);

            var gastosPorMês = GerarGastosPorMes(despesasDoAno);

            var gastosPorCategoria = GerarGastosPorCategoria(despesasDoAno);

            var output = new ResumoDeGastosOutput() { 
                TotalGastoAno = gastoAnual,
                TotalGastoMes = gastoMensal,
                GastosPorMes = gastosPorMês,
                GastosPorCategoria = gastosPorCategoria
            };

            return new Resultado<ResumoDeGastosOutput>(null, output);
        }
        private List<GastoPorMes> GerarGastosPorMes(List<Despesa> despesasDoAno)
        {
            List<GastoPorMes> gastos = [
            new GastoPorMes {Mes = "Jan", Valor = 0},
            new GastoPorMes {Mes = "Fev", Valor = 0},
            new GastoPorMes {Mes = "Mar", Valor = 0},
            new GastoPorMes {Mes = "Abr", Valor = 0},
            new GastoPorMes {Mes = "Mai", Valor = 0},
            new GastoPorMes {Mes = "Jun", Valor = 0},
            new GastoPorMes {Mes = "Jul", Valor = 0},
            new GastoPorMes {Mes = "Ago", Valor = 0},
            new GastoPorMes {Mes = "Set", Valor = 0},
            new GastoPorMes {Mes = "Out", Valor = 0},
            new GastoPorMes {Mes = "Nov", Valor = 0},
            new GastoPorMes {Mes = "Dez", Valor = 0}
            ];

            foreach (var desp in despesasDoAno)
            {
                gastos[desp.Data.Month - 1].Valor += desp.Valor;
            }

            return gastos;
        }

        private List<GastoPorCategoria> GerarGastosPorCategoria(List<Despesa> despesasDoAno)
        {
            List<GastoPorCategoria> gastos = [
                new GastoPorCategoria {Categoria = CategoriaEnum.Nenhuma, Valor = 0},
                new GastoPorCategoria {Categoria = CategoriaEnum.Alimentacao, Valor = 0},
                new GastoPorCategoria {Categoria = CategoriaEnum.Lazer, Valor = 0},
                new GastoPorCategoria {Categoria = CategoriaEnum.Roupas, Valor = 0},
                new GastoPorCategoria {Categoria = CategoriaEnum.Saude, Valor = 0},
                new GastoPorCategoria {Categoria = CategoriaEnum.Outra, Valor = 0},
            ];

            foreach (var desp in despesasDoAno)
            {
                gastos[((int)desp.Categoria)].Valor += desp.Valor;
            }

            return gastos;
        }
    };    
}
