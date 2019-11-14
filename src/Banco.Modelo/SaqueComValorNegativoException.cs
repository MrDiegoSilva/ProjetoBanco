using System;

namespace Banco.Modelo
{
    public class SaqueComValorNegativoException : Exception
    {
        public SaqueComValorNegativoException(string message) : base(message)
        {

        }
    }
}
