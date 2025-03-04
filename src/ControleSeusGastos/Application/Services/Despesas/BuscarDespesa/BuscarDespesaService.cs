using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.BuscarDespesa.DTO;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.BuscarDespesa
{
    internal class BuscarDespesaService : IBuscarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        public BuscarDespesaService(IDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }

        public async Task<BuscarDespesaOutput?> BuscarPorId(int idDespesa)
        {
            var despesa = await _despesaRepository.buscaPorId(idDespesa);

            if (despesa == null)
            {
                return null;
            }

            var despesaDTO = new BuscarDespesaOutput
            {
                Data = despesa.Data,
                Descricao = despesa.Descricao,
                Nome = despesa.Nome
            };

            return despesaDTO;
        }

        public async Task<List<BuscarDespesaOutput>?> BuscarPorIdUsuario(int IdUsuario)
        {
            var despesas = await _despesaRepository.buscarPorIdUsuario(IdUsuario);

            if (despesas == null || despesas.Count <= 0)
            {
                return null;
            }

            List<BuscarDespesaOutput> resul = [];

            foreach (var desp in despesas)
            {
                resul.Add(new BuscarDespesaOutput
                {
                    Data = desp.Data,
                    Descricao = desp.Descricao,
                    Nome = desp.Nome
                });
            }


            return resul;
        }

        public async Task<List<BuscarDespesaOutput>?> BuscarPorPeriodo(BuscarPorPeriodoInput input)
        {
            var despesas = await _despesaRepository.buscarPorPeriodo(input.idUsuario, input.DataInicio, input.DataFim);

            if (despesas == null || despesas.Count <= 0)
            {
                return null;
            }

            List<BuscarDespesaOutput> resul = [];

            foreach (var desp in despesas)
            {
                resul.Add(new BuscarDespesaOutput
                {
                    Data = desp.Data,
                    Descricao = desp.Descricao,
                    Nome = desp.Nome
                });
            }
            return resul;
        }
    }
}
