using System;

namespace TransacaoFinanceira.Exceptions
{
	public class TransferenciaInvalidaException : Exception
	{
		public TransferenciaInvalidaException() : base("Transferência inválida")
		{
		}

		public TransferenciaInvalidaException(string message) : base(message)
		{
		}
	}
}