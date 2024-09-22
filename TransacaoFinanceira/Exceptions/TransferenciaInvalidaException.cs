using System;

namespace TransacaoFinanceira.Exceptions
{
	public class TransferenciaInvalidaException : Exception
	{
		public TransferenciaInvalidaException() : base("Transfer�ncia inv�lida")
		{
		}

		public TransferenciaInvalidaException(string message) : base(message)
		{
		}
	}
}