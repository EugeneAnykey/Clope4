using System;

namespace ClopeLib
{
	public interface ITransaction : IEquatable<ITransaction>
	{
		uint Id { get; }

		int Length { get; }

		int[] Links { get; }
	}
}
