using Application.Services.Despesas.CriarDespesa.DTO;
using Domain.Despesas;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.CriarDespesa
{
    internal class CriarDespesaService : ICriarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly CriarDespesaValidador _validador;
        
        public CriarDespesaService(IDespesaRepository despesaRepository,
            CriarDespesaValidador validador)
        {
            _despesaRepository = despesaRepository;
            _validador = validador;
        }

        public async Task<Resultado<CriarDespesaOutput>> CriarNovaDespesa(CriarDespesaInput input)
        {

            var erros = await _validador.validar(input);

            if(erros.Count > 0)
            {
                var erroResul = new Resultado<CriarDespesaOutput>(erros, null);
                return erroResul;
            }

            var novaDespesa = new Despesa()
            {
                Nome = input.Nome.Trim(),
                Valor = input.Valor,
                Descricao = input.Descricao,
                Categoria = input.Categoria,
                Data = input.Data.ToUniversalTime(),
                Usuario_Id = input.Usuario_Id
            };

            await _despesaRepository.criar(novaDespesa);;

            var despesaOutput = new CriarDespesaOutput()
            {
                Nome = novaDespesa.Nome,
                Valor = novaDespesa.Valor,
                Descricao = novaDespesa.Descricao,
                Categoria = Enum.GetName(novaDespesa.Categoria),
                Data = novaDespesa.Data
            };

            var resultado = new Resultado<CriarDespesaOutput>(null, despesaOutput);

            return resultado;
        }
    }
}
