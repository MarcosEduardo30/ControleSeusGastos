using Application.Services.Despesas.EditarDespesa;
using Application.Services.Despesas.EditarDespesa.DTO;
using Application.Validacao;
using Moq;

namespace ControleSeusGastos.Unit.Tests.Application.Despesa
{
    public class EditarDespesaValidadorTests
    {

        private readonly Mock<IValidadorDatabase> _validadorDatabaseMock;
        private readonly EditarDespesaValidador SUT;

        public EditarDespesaValidadorTests()
        {
            _validadorDatabaseMock = new Mock<IValidadorDatabase>();
            SUT = new EditarDespesaValidador( _validadorDatabaseMock.Object );
        }


        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidos_AoValidarEditarDespesa()
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.Empty(result);
            Assert.IsType<List<Erro>>(result);
            _validadorDatabaseMock.Verify(service => service.ExisteDespesaDB(1), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidosEDescricaoNula_AoValidarEditarDespesa()
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.Empty(result);
            Assert.IsType<List<Erro>>(result);
            _validadorDatabaseMock.Verify(service => service.ExisteDespesaDB(1), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidosECategoriaNula_AoValidarEditarDespesa()
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.Empty(result);
            Assert.IsType<List<Erro>>(result);
            _validadorDatabaseMock.Verify(service => service.ExisteDespesaDB(1), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void DeveriaRetornarFalse_QueValorInvalido_AoValidarEditarDespesa(int valor)
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = valor,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Single<Erro>(result);
            Assert.Equal("Valor_Invalido", result[0].nome);
            Assert.Equal("Valor da despesa deve ser maior do que zero", result[0].mensagem);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("       ")]
        public async void DeveriaRetornarFalse_QueNomeVazio_AoValidarEditarDespesa(string valor)
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = valor,
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Single<Erro>(result);
            Assert.Equal("Nome_Vazio", result[0].nome);
            Assert.Equal("Campo nome não pode ser vazio", result[0].mensagem);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("bc")]
        [InlineData("a       ")]
        public async void DeveriaRetornarFalse_QueNomeMuitoCurto_AoValidarEditarDespesa(string valor)
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = valor,
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Single<Erro>(result);
            Assert.Equal("Nome_Curto", result[0].nome);
            Assert.Equal("Nome da despesa deve ser maior que 3 caracteres", result[0].mensagem);
        }

        [Fact]
        public async void DeveriaRetornarFalse_QueCategoriaNaoExiste_AoValidarEditarDespesa()
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(true));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Nome",
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input, 1);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Single<Erro>(result);
            Assert.Equal("Categoria_Invalida", result[0].nome);
            Assert.Equal("A Categoria da despesa não existe", result[0].mensagem);
        }

        [Fact]
        public async void DeveriaRetornarFalse_QueDespesaNaoExiste_AoValidarEditarDespesa()
        {
            //Arrange
            _validadorDatabaseMock.Setup(service => service.ExisteDespesaDB(1)).Returns(Task.FromResult(false));

            var input = new EditarDespesaInput
            {
                Valor = 100,
                Nome = "Nome",
                Descricao = "Descrição teste",
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var resul = await SUT.validar(input, 1);

            //Assert
            Assert.IsType<List<Erro>>(resul);
            Assert.Single<Erro>(resul);
            Assert.Equal("despesa_invalida", resul[0].nome);
            Assert.Equal("Despesa não foi encontrada", resul[0].mensagem);

        }
    }
}
