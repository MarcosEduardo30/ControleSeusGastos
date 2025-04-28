using Application.Services.Despesas.ExcluirDespesa;
using Application.Validacao;
using Infrastructure.Repositories.Depesas;
using Moq;

namespace ControleSeusGastos.Unit.Tests.Application.Despesa
{
    public class ExcluirDespesaServiceTests
    {
        private readonly Mock<IDespesaRepository> _despesaRepository;
        private readonly Mock<IValidadorDatabase> _validadorDatabase;
        private readonly ExcluirDespesaService SUT;

        public ExcluirDespesaServiceTests()
        {
            _despesaRepository = new Mock<IDespesaRepository>();
            _validadorDatabase = new Mock<IValidadorDatabase>();
            SUT = new ExcluirDespesaService(_despesaRepository.Object, _validadorDatabase.Object);
        }


        [Fact]
        public async Task DeveriaRetornarTrue_QuandoIdDespesaValido()
        {
            var input = 1;

            _validadorDatabase
                .Setup(v => v.ExisteDespesaDB(It.IsAny<int>()))
                .ReturnsAsync(true);

            _despesaRepository
                .Setup(r => r.removerPorId(It.IsAny<int>()))
                .ReturnsAsync(1);

            var result = await SUT.Excluir(input);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveriaRetornarFalse_QuandoIdDespesaInvalido()
        {
            var input = 1;

            _validadorDatabase
                .Setup(v => v.ExisteDespesaDB(It.IsAny<int>()))
                .ReturnsAsync(false);

            _despesaRepository
                .Setup(r => r.removerPorId(It.IsAny<int>()))
                .ReturnsAsync(0);

            var result = await SUT.Excluir(input);

            Assert.False(result);
        }
    }
}
