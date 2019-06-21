using System;
using System.Collections.Generic;
using System.IO;

namespace ClopeLib.Readers
{
	public class SimpleReader : IPortionReader
	{
		// field
		readonly string filename;

		List<string> lines = null;

		int linesReaded = 0;
		int linesLeft = 0;

		public bool ReachedEndOfFile { get; private set; }

		public int LinesToReadAtOnce { get; set; }



		// init
		public SimpleReader(string filename)
		{
			this.filename = filename ?? throw new ArgumentNullException();
			if (!File.Exists(filename))
				throw new FileNotFoundException();

			LinesToReadAtOnce = 10;
			ReachedEndOfFile = false;
		}



		// read from file
		void ReadFromFileOnce()
		{
			lines = new List<string>();
			lines.AddRange(File.ReadAllLines(filename));
			linesLeft = lines.Count;
		}



		// IReader
		public List<string> GetData() => GetData(LinesToReadAtOnce);

		public List<string> GetData(int linesToRead)
		{
			if (lines == null)
			{
				ReadFromFileOnce();
			}

			CheckNumbers(ref linesToRead);

			var res = new List<string>();

			if (linesToRead == 0)
				return res;

			res.AddRange(GetStrings(linesToRead));

			return res;
		}



		// private
		string[] GetStrings(int linesToRead)
		{
			string[] res = new string[linesToRead];
			lines.CopyTo(linesReaded, res, 0, linesToRead);
			linesReaded += linesToRead;
			return res;
		}



		void CheckNumbers(ref int linesToRead)
		{
			if (linesToRead <= 0)
			{
				linesToRead = 0;
				return;
			}

			if (linesLeft - linesToRead > 0)
			{
				linesLeft -= linesToRead;
			}
			else
			{
				linesToRead = linesLeft;
				linesLeft = 0;
				ReachedEndOfFile = true;
			}
		}
	}
}
