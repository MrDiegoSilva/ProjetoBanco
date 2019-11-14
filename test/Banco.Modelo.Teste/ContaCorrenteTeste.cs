using Moq;
using Xunit;

namespace Banco.Modelo.Teste
{
    // Classe que simula uma conta bancária

    // Consultar o Saldo
    // Depositar o Dinheiro
    // Sacar o Dinheiro
    // Não permitir saque superior aos fundos
    // Não permitir saque de valor negativo

    public class ContaCorrenteTeste
    {
        private readonly ContaCorrente _conta;

        public ContaCorrenteTeste()
        {
            _conta = new ContaCorrente("0011", false);
        }

        [Fact]
        public void DeveConsultarSaldoComValorZero()
        {
            // Arrange

            // Act
            decimal saldo = _conta.ConsultarSaldo();

            // Assert
            Assert.Equal(0, saldo);
        }

        [Fact]
        public void DeveDeposisitarValorDe250Reais()
        {
            // Arrange
            decimal valorDeDeposito = 250m;

            // Act
            _conta.Depositar(valorDeDeposito);
            decimal saldo = _conta.ConsultarSaldo();

            // Assert
            Assert.Equal(valorDeDeposito, saldo);
        }

        [Fact]
        public void DeveSacarValorDe250Reais()
        {
            // Arrange
            decimal valorDeDeposito = 250m;
            decimal valorDeSaque = 250m;
            decimal saldoEsperado = valorDeDeposito - valorDeSaque;
            // Act
            _conta.Depositar(valorDeDeposito);
            _conta.Sacar(valorDeSaque);
            decimal saldo = _conta.ConsultarSaldo();

            // Assert
            Assert.Equal(saldoEsperado, saldo);
        }

        [Fact]
        public void DeveBloquearSaqueQuandoQuandoValorEstaAcimaDoSaldo()
        {
            // Arrange
            decimal valorDeDeposito = 250m;
            decimal valorDeSaque = 300m;

            // Act
            _conta.Depositar(valorDeDeposito);
            _conta.Sacar(valorDeSaque);
            decimal saldo = _conta.ConsultarSaldo();

            // Assert
            Assert.Equal(valorDeDeposito, saldo);
        }

        [Fact]
        public void DeveBloquearSaqueQuandoQuandoValorENegativo()
        {
            // Arrange
            decimal valorDeDeposito = 250m;
            decimal valorDeSaque = -250m;

            // Act
            _conta.Depositar(valorDeDeposito);
            var exception = Assert.Throws<SaqueComValorNegativoException>(() => _conta.Sacar(valorDeSaque));

            decimal saldo = _conta.ConsultarSaldo();
            // Assert
            Assert.Equal("Saque não pode ser de valor negativo!", exception.Message);
            Assert.Equal(valorDeDeposito, saldo);
        }

        [Fact]
        public void NaoDeveRetornarContaInativaAoBuscarPorNumero()
        {
            // Arrange
            var contaMock = new Mock<IContaCorrenteRepositorio>();

            // Act
            contaMock.Setup(x => x.BuscarContaPorNumero("00111"));
            var conta = contaMock.Object.BuscarContaPorNumero("00111");

            // Assert
            Assert.Null(conta);
        }

        [Fact]
        public void DeveRetornarContaAtivaAoBuscarPorNumero()
        {
            // Arrange
            var contaMock = new Mock<IContaCorrenteRepositorio>();
            var contaResultado = new ContaCorrente("00225", true);

            // Act
            contaMock.Setup(x => x.BuscarContaPorNumero("00225")).Returns(contaResultado);
            var conta = contaMock.Object.BuscarContaPorNumero("00225");

            // Assert
            Assert.NotNull(conta);
            Assert.True(conta.Status);
        }
    }
}
