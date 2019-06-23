using System;

namespace ClopeLib
{
	public interface IAttribute : IEquatable<IAttribute>
	{
		int Id { get; }
		int Index { get; }
		string Name { get; }
	}
}
