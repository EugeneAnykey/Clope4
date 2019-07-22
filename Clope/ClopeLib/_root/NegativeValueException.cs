using System;

namespace ClopeLib
{
	public class NegativeValueException : Exception
	{
		public NegativeValueException() : base("Value should be greater or equal to zero") { }

		public NegativeValueException(string message) : base(message) { }
	}
}
