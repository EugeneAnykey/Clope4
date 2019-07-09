using System;

namespace ClopeLib
{
	public interface ITransaction : IEquatable<ITransaction>
	{
		/// <summary>
		/// Unique transaction id
		/// </summary>
		uint Id { get; }

		/// <summary>
		/// Count of linked attributes
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Links of attributes
		/// </summary>
		int[] Links { get; }
	}
}
