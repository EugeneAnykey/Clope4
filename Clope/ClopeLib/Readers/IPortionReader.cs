using System.Collections.Generic;

namespace ClopeLib.Readers
{
	public interface IPortionReader : IReader
	{
		int LinesToReadAtOnce { get; set; }

		List<string> GetData(int linesToRead);
	}
}
