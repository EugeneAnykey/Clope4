using System;

namespace ClopeLib
{
	public interface IAttribute : IEquatable<IAttribute>
	{
		int Position { get; }

		string Name { get; }
	}
}
