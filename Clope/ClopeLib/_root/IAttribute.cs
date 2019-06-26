using System;

namespace ClopeLib
{
	public interface IAttribute : IEquatable<IAttribute>
	{
		int Id { get; }
		int Position { get; }
		string Name { get; }
	}
}
