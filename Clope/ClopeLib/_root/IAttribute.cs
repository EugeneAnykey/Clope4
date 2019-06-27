using System;

namespace ClopeLib
{
	public interface IAttribute : IEquatable<IAttribute>
	{
		int Link { get; set; }
		int Position { get; }
		string Name { get; }
	}
}
