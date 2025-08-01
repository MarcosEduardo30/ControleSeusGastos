using Application.Services.Despesas.BuscarDespesa;
using Domain.Despesas;
using Domain.Usuarios;
using Infrastructure.Repositories.Depesas;
using Moq;

namespace ControleSeusGastos.Unit.Tests.Application.Despesa
{
    public class BuscarDespesaServiceTests
    {
        private readonly Mock<IDespesaRepository> _mockDespesaRepository;
        private readonly BuscarDespesaService _sut;

        public BuscarDespesaServiceTests() { 
            _mockDespesaRepository = new Mock<IDespesaRepository>();
            _sut = new BuscarDespesaService(_mockDespesaRepository.Object);
        }

        [Fact]
        public async void DeveriaRetornarDespesa_QuandoIdValido_AoBuscarPorId()
        {
            //Arrange
            int input = 1;

            var mockDespesa = Mock.Of<Domain.Despesas.Despesa>();
            mockDespesa.Usuario = Mock.Of<Usuario>();

            _mockDespesaRepository
                .Setup(m => m.buscaPorId(It.IsAny<int>()))
                .ReturnsAsync(mockDespesa);

            //Act
            var result = await _sut.BuscarPorId(input);

            //Assert
            Assert.NotNull(result);
            _mockDespesaRepository.Verify(m => m.buscaPorId(input), Times.Once);
        }


        [Fact]
        public async void DeveriaRetornarDespesa_QuandoDespesaNaoPossuiCategoria_AoBuscarPorId()
        {
            //Arrange
            int input = 1;

            var mockDespesa = Mock.Of<Domain.Despesas.Despesa>();
            mockDespesa.Usuario = Mock.Of<Usuario>();

            _mockDespesaRepository
                .Setup(m => m.buscaPorId(It.IsAny<int>()))
                .ReturnsAsync(mockDespesa);

            //Act
            var result = await _sut.BuscarPorId(input);

            //Assert
            Assert.NotNull(result);
            _mockDespesaRepository.Verify(m => m.buscaPorId(input), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarNulo_QuandoDespesaNaoEncontrada_AoBuscarPorId()
        {
            //Arrange
            int input = 1;

            _mockDespesaRepository
                .Setup(m => m.buscaPorId(It.IsAny<int>()))
                .ReturnsAsync((Domain.Despesas.Despesa)null);

            //Act
            var result = await _sut.BuscarPorId(input);

            //Assert
            Assert.Null(result);
            _mockDespesaRepository.Verify(m => m.buscaPorId(input), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarDespesas_QuandoIdValido_AoBuscarPorIdUsuario()
        {

            //Arrange
            int input = 1;

            var mockDespesa = Mock.Of<Domain.Despesas.Despesa>();
            mockDespesa.Usuario = Mock.Of<Usuario>();
            List<Domain.Despesas.Despesa> listDespesa = [];
            listDespesa.Add(mockDespesa);

            _mockDespesaRepository
                .Setup(m => m.buscarPorIdUsuario(It.IsAny<int>()))
                .ReturnsAsync(listDespesa);

            //Act
            var result = await _sut.BuscarPorIdUsuario(input);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _mockDespesaRepository.Verify(m => m.buscarPorIdUsuario(input), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarNulo_QuandoIdNaoTemDespesas_AoBuscarPorIdUsuario()
        {

            //Arrange
            int input = 1;

            _mockDespesaRepository
                .Setup(m => m.buscarPorIdUsuario(It.IsAny<int>()))
                .ReturnsAsync((List<Domain.Despesas.Despesa>)null);

            //Act
            var result = await _sut.BuscarPorIdUsuario(input);

            //Assert
            Assert.Null(result);
            _mockDespesaRepository.Verify(m => m.buscarPorIdUsuario(input), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarDespesas_QuandoIdTemDespesas_AoBuscarPorPeriodo()
        {
            //Arrange
            int idUsuario = 1;
            DateTime DataInicio = DateTime.Now;
            DateTime DataFim = DateTime.Now.AddDays(1);

            var mockDespesa = Mock.Of<Domain.Despesas.Despesa>();
            mockDespesa.Usuario = Mock.Of<Usuario>();
            List<Domain.Despesas.Despesa> listDespesa = [];
            listDespesa.Add(mockDespesa);

            _mockDespesaRepository
                .Setup(m => m.buscarPorPeriodo(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(listDespesa);

            //Act
            var result = await _sut.BuscarPorPeriodo(idUsuario, DataInicio, DataFim);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _mockDespesaRepository.Verify(m => m.buscarPorPeriodo(idUsuario, DataInicio, DataFim), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarNulo_QuandoIdNaoTemDespesas_AoBuscarPorPeriodo()
        {
            //Arrange
            int idUsuario = 1;
            DateTime DataInicio = DateTime.Now;
            DateTime DataFim = DateTime.Now.AddDays(1);

            _mockDespesaRepository
                .Setup(m => m.buscarPorPeriodo(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((List<Domain.Despesas.Despesa>)null);

            //Act
            var result = await _sut.BuscarPorPeriodo(idUsuario, DataInicio, DataFim);

            //Assert
            Assert.Null(result);
            _mockDespesaRepository.Verify(m => m.buscarPorPeriodo(idUsuario, DataInicio, DataFim), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarNulo_QuandoDataFimMaiorQueDataInicio_AoBuscarPorPeriodo()
        {
            //Arrange
            int idUsuario = 1;
            DateTime DataInicio = DateTime.Now.AddDays(1);
            DateTime DataFim = DateTime.Now;

            //Act
            var result = await _sut.BuscarPorPeriodo(idUsuario, DataInicio, DataFim);

            //Assert
            Assert.Null(result);
            _mockDespesaRepository.Verify(m => m.buscarPorPeriodo(idUsuario, DataInicio, DataFim), Times.Never);
        }

    }
}
