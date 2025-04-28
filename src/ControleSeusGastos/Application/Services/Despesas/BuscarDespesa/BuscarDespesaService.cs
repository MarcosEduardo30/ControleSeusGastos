using Application.Despesas.BuscarDespesa.DTO;
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
                Id = despesa.Id,
                Data = despesa.Data,
                Valor = despesa.Valor,
                Descricao = despesa.Descricao,
                Usuario_Nome = despesa.Usuario.username,
                Nome = despesa.Nome
            };

            if (despesa.Categoria != null)
            {
                despesaDTO.Categoria_Nome = despesa.Categoria.nome;
            }            

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
                    Id = desp.Id,
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

        public async Task<List<BuscarDespesaOutput>?> BuscarPorPeriodo(int idUsuario, DateTime DataInicio, DateTime DataFim)
        {
            if(DataInicio > DataFim)
            {
                return null;
            }

            var despesas = await _despesaRepository.buscarPorPeriodo(idUsuario, DataInicio, DataFim);

            if (despesas == null || despesas.Count <= 0)
            {
                return null;
            }

            List<BuscarDespesaOutput> resul = [];

            foreach (var desp in despesas)
            {
                var output = new BuscarDespesaOutput
                {
                    Id = desp.Id,
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
