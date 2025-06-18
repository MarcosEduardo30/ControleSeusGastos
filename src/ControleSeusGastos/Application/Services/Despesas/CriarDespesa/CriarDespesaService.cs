using Application.Services.Despesas.CriarDespesa.DTO;
using Domain.Categorias;
using Domain.Despesas;
using Infrastructure.Repositories.Categorias;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.CriarDespesa
{
    internal class CriarDespesaService : ICriarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly CriarDespesaValidador _validador;
        
        public CriarDespesaService(IDespesaRepository despesaRepository,
            CriarDespesaValidador validador,
            ICategoriaRepository categoriaRepository)
        {
            _despesaRepository = despesaRepository;
            _validador = validador;
            _categoriaRepository = categoriaRepository;
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
                Data = input.Data.ToUniversalTime(),
                Categoria_Id = input.Categoria_Id,
                Usuario_Id = input.Usuario_Id
            };

            await _despesaRepository.criar(novaDespesa);


            string categoriaNome = "";
            if (novaDespesa.Categoria_Id is not null)
            {
                var categoria = await _categoriaRepository.buscarPorId((int)novaDespesa.Categoria_Id);
                categoriaNome = categoria.nome;
            }


            var despesaOutput = new CriarDespesaOutput()
            {
                Nome = novaDespesa.Nome,
                Valor = novaDespesa.Valor,
                Descricao = novaDespesa.Descricao,
                Categoria_Nome = categoriaNome,
                Data = novaDespesa.Data
            };

            var resultado = new Resultado<CriarDespesaOutput>(null, despesaOutput);

            return resultado;
        }
    }
}
