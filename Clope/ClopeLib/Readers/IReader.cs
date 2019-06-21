using System.Collections.Generic;

namespace ClopeLib.Readers
{
	public interface IReader
	{
		bool ReachedEndOfFile { get; }

		List<string> GetData();
	}
}
