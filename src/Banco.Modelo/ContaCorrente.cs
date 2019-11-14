using System;

namespace Banco.Modelo
{
    public class ContaCorrente
    {
        public ContaCorrente(string numero, bool status)
        {
            Numero = numero;
            Status = status;
        }

        public ContaCorrente(string numero, bool status, decimal saldoBloqueado, decimal saldoDisponivel, decimal saldo)
        {
            Numero = numero;
            Status = status;
            Saldo = saldo;
            SaldoBloqueado = saldoBloqueado;
            SaldoDisponivel = saldoDisponivel;
        }

        public string Numero { get; private set; }

        public bool Status { get; private set; }

        public decimal SaldoBloqueado { get; private set; }

        public decimal SaldoDisponivel { get; private set; }

        public decimal Saldo { get; private set; }

        public void Depositar(decimal valor)
        {
            Saldo += valor;
        }

        public decimal ConsultarSaldo()
        {
            return ComporSaldoDisponivel();
        }

        private decimal ComporSaldoDisponivel()
        {
            SaldoDisponivel = Saldo - SaldoBloqueado;
            return SaldoDisponivel;
        }

        public void Sacar(decimal valorDeSaque)
        {
            if (valorDeSaque < 0)
                throw new SaqueComValorNegativoException("Saque não pode ser de valor negativo!");

            if (valorDeSaque <= ComporSaldoDisponivel())
                Saldo -= valorDeSaque;
        }
    }
}
