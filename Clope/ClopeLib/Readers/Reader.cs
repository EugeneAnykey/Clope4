using System;
using System.Collections.Generic;
using System.IO;

namespace ClopeLib.Readers
{
	public class Reader : IPortionReader, IDisposable
	{
		// field
		readonly StreamReader reader;

		public bool ReachedEndOfFile => reader.EndOfStream;

		public int LinesToReadAtOnce { get; set; }



		// init
		Reader()
		{
			LinesToReadAtOnce = 10;
		}

		public Reader(string filename) : this()
		{
			if (filename == null)
				throw new ArgumentNullException();

			reader = File.Exists(filename) ? new StreamReader(filename) : throw new FileNotFoundException();
		}

		public Reader(Stream stream) : this()
		{
			if (stream == null)
				throw new ArgumentNullException();

			reader = new StreamReader(stream);
		}

		public void Dispose()
		{
			if (reader != null)
			{
				reader.Dispose();
			}
		}



		// IReader, IPortionReader
		public List<string> GetData() => GetData(LinesToReadAtOnce);

		public List<string> GetData(int linesToRead)
		{
			var result = new List<string>();
			int i = linesToRead;

			while (!reader.EndOfStream && i-- > 0)
			{
				result.Add(reader.ReadLine());
			}

			return result;
		}
	}
}
