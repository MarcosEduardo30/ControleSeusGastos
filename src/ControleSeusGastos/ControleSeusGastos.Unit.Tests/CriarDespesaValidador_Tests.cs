using Application.Services.Despesas.CriarDespesa;
using Application.Services.Despesas.CriarDespesa.DTO;
using Application.Validacao;
using Moq;

namespace ControleSeusGastos.Unit.Tests
{
    public class CriarDespesaValidador_Tests
    {
        private readonly Mock<IValidadorDatabase> _ValidadorDatabaseMock;
        private readonly CriarDespesaValidador SUT;

        public CriarDespesaValidador_Tests()
        {
            _ValidadorDatabaseMock = new Mock<IValidadorDatabase>();
            SUT = new CriarDespesaValidador(_ValidadorDatabaseMock.Object);
        }


        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidos_AoValidarCriarDespesa()
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Empty(result);
            _ValidadorDatabaseMock.Verify(service => service.ExisteUsuarioDB(1), Times.Once);
            _ValidadorDatabaseMock.Verify(service => service.ExisteCategoriaDB(1), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidosEDescricaoNula_AoValidarCriarDespesa()
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Empty(result);
            _ValidadorDatabaseMock.Verify(service => service.ExisteUsuarioDB(1), Times.Once);
            _ValidadorDatabaseMock.Verify(service => service.ExisteCategoriaDB(1), Times.Once);
        }

        [Fact]
        public async void DeveriaRetornarTrue_QueDadosValidosECategoriaNula_AoValidarCriarDespesa()
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            //Assert
            Assert.IsType<List<Erro>>(result);
            Assert.Empty(result);
            _ValidadorDatabaseMock.Verify(service => service.ExisteUsuarioDB(1), Times.Once);
            _ValidadorDatabaseMock.Verify(service => service.ExisteCategoriaDB(1), Times.Never);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void DeveriaRetornarFalse_QueValorInvalido_AoValidarCriarDespesa(int valor)
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = valor,
                Nome = "Teste",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            Assert.IsType<List<Erro>>(result);
            Assert.Single(result);
            Assert.Equal("Valor_Invalido", result[0].nome);
            Assert.Equal("Valor da despesa deve ser maior do que zero", result[0].mensagem);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("       ")]
        public async void DeveriaRetornarFalse_QueNomeNulo_AoValidarCriarDespesa(string nome)
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = nome,
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            Assert.IsType<List<Erro>>(result);
            Assert.Single(result);
            Assert.Equal("Nome_Vazio", result[0].nome);
            Assert.Equal("Campo nome não pode ser vazio", result[0].mensagem);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("bc")]
        [InlineData("a       ")]
        public async void DeveriaRetornarFalse_QueNomeMuitoCurto_AoValidarCriarDespesa(string nome)
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = nome,
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            Assert.IsType<List<Erro>>(result);
            Assert.Single(result);
            Assert.Equal("Nome_Curto", result[0].nome);
            Assert.Equal("Nome da despesa deve ser maior que 3 caracteres", result[0].mensagem);
        }

        [Fact]
        public async void DeveriaRetornarFalse_QueCategoriaNaoExiste_AoValidarCriarDespesa()
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(true));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(false));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Nome",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            Assert.IsType<List<Erro>>(result);
            Assert.Single(result);
            Assert.Equal("Categoria_Invalida", result[0].nome);
            Assert.Equal("A Categoria da despesa não existe", result[0].mensagem);
        }

        [Fact]
        public async void DeveriaRetornarFalse_QueUsuarioNaoExiste_AoValidarCriarDespesa()
        {
            //Arrange
            _ValidadorDatabaseMock.Setup(service => service.ExisteUsuarioDB(1)).Returns(Task.FromResult(false));
            _ValidadorDatabaseMock.Setup(service => service.ExisteCategoriaDB(1)).Returns(Task.FromResult(true));
            var input = new CriarDespesaInput
            {
                Valor = 100,
                Nome = "Nome",
                Descricao = "Descrição teste",
                Categoria_Id = 1,
                Usuario_Id = 1,
                Data = new DateTime(2025, 01, 01)
            };

            //Act
            var result = await SUT.validar(input);

            Assert.IsType<List<Erro>>(result);
            Assert.Single(result);
            Assert.Equal("Usuario_Invalido", result[0].nome);
            Assert.Equal("Usuário da despesa não está cadastrado", result[0].mensagem);
        }
    }
}
