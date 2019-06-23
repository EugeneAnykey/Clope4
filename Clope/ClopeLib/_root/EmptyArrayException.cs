using System;

namespace ClopeLib
{
	public class EmptyArrayException : Exception
	{
		public EmptyArrayException() : base("Array should have one or more items") { }

		public EmptyArrayException(string message) : base(message) { }
	}
}
