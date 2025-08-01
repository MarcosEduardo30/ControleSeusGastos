using Application.Services.Despesas.EditarDespesa.DTO;
using Infrastructure.Repositories.Depesas;

namespace Application.Services.Despesas.EditarDespesa
{
    internal class EditarDespesaService : IEditarDespesaService
    {
        private readonly IDespesaRepository _despesaRepository;
        private readonly EditarDespesaValidador _validador;

        public EditarDespesaService(IDespesaRepository despesaRepository, EditarDespesaValidador validador)
        {
            _despesaRepository = despesaRepository;
            _validador = validador;
        }
        public async Task<Resultado<EditarDespesaOutput>> Editar(int id,EditarDespesaInput NovaDespesa)
        {
            var erros = await _validador.validar(NovaDespesa, id);
            if (erros.Count > 0)
            {
                var erroResul = new Resultado<EditarDespesaOutput>(erros, null);
                return erroResul;
            }

            var despesaAntiga = await _despesaRepository.buscaPorId(id);
            despesaAntiga.Nome = NovaDespesa.Nome.Trim();
            despesaAntiga.Valor = NovaDespesa.Valor;
            despesaAntiga.Descricao = NovaDespesa.Descricao;
            despesaAntiga.Categoria = NovaDespesa.Categoria;
            despesaAntiga.Data = NovaDespesa.Data.ToUniversalTime();

            await _despesaRepository.atualizar(despesaAntiga);

            var output = new EditarDespesaOutput
            {
                Nome = despesaAntiga.Nome,
                Valor = despesaAntiga.Valor,
                Descricao = despesaAntiga.Descricao,
                Data = despesaAntiga.Data,
            };


            var resul = new Resultado<EditarDespesaOutput>(null, output);
            return resul;
        }
    }
}
