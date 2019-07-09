using System;

namespace ClopeLib
{
	public interface IAttribute : IEquatable<IAttribute>
	{
		/// <summary>
		/// Attribute column index
		/// </summary>
		int Position { get; }

		/// <summary>
		/// Attribute column value
		/// </summary>
		string Name { get; }
	}
}
