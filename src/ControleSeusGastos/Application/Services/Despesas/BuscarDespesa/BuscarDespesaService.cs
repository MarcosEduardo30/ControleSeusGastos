using Application.Despesas.BuscarDespesa.DTO;
using Application.Services.Despesas.BuscarDespesa.DTO;
using Domain.Despesas;
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
                Valor = despesa.Valor,
                Descricao = despesa.Descricao,
                Categoria_Nome = despesa.Categoria.nome,
                Usuario_Nome = despesa.Usuario.username,
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
                var output = new BuscarDespesaOutput
                {
                    Data = desp.Data,
                    Valor = desp.Valor,
                    Descricao = desp.Descricao,
                    Usuario_Nome = desp.Usuario.username,
                    Nome = desp.Nome
                };

                output.Categoria_Nome = desp.Categoria != null ? desp.Categoria.nome : null;

                resul.Add(output);
            }


            return resul;
        }

        public async Task<List<BuscarDespesaOutput>?> BuscarPorPeriodo(int idUsuario, BuscarPorPeriodoInput input)
        {
            var despesas = await _despesaRepository.buscarPorPeriodo(idUsuario, input.DataInicio, input.DataFim);

            if (despesas == null || despesas.Count <= 0)
            {
                return null;
            }

            List<BuscarDespesaOutput> resul = [];

            foreach (var desp in despesas)
            {
                var output = new BuscarDespesaOutput
                {
                    Data = desp.Data,
                    Valor = desp.Valor,
                    Descricao = desp.Descricao,
                    Usuario_Nome = desp.Usuario.username,
                    Nome = desp.Nome
                };

                output.Categoria_Nome = desp.Categoria != null ? desp.Categoria.nome : null;

                resul.Add(output);
            }
            return resul;
        }
    }
}
