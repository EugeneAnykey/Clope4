using System;

namespace ClopeLib
{
	public interface IAttribute<T> : IEquatable<IAttribute<T>>
	{
		/// <summary>
		/// Attribute column value
		/// </summary>
		T Value { get; }
	}
}
