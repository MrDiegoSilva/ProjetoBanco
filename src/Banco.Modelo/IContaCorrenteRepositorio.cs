namespace Banco.Modelo
{
    public interface IContaCorrenteRepositorio
    {
        ContaCorrente BuscarContaPorNumero(string numero);
    }
}
