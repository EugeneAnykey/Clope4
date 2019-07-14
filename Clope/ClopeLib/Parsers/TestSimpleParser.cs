using System;
using System.Collections.Generic;

namespace ClopeLib.Parsers
{
	public class TestSimpleParser : IParser
	{
		// const
		public static readonly char[] CommaSplitter = { ',' };
		public static readonly char[] CommaSpaceSplitter = { ',', ' ' };
		public static readonly char[] SemicolonSplitter = { ';' };
		public static readonly char[] SemicolonSpaceSplitter = { ';', ' ' };

		// field
		public char[] Splitter { get; set; }

		readonly int timesMultiply = 0;



		// init
		public TestSimpleParser() : this(CommaSplitter) { }

		public TestSimpleParser(char[] splitter, int timesMultiply = 1)
		{
			Splitter = splitter;
			this.timesMultiply = timesMultiply;
		}



		//  public
		public string[] Parse(string line)
		{
			var result = line.Split(Splitter, StringSplitOptions.RemoveEmptyEntries);

			var invalids = new[] { "?" };

			InvalidDataCorrection(result, invalids, null);

			if (timesMultiply > 1)
			{
				List<string> list = new List<string>();
				for (int i = 0; i < timesMultiply; i++)
					list.AddRange(result);
				return list.ToArray();
			}
			else
				return result;
		}



		// private
		void InvalidDataCorrection(string[] lines, string[] invalids, string replacementForInvalid)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				foreach (var invalid in invalids)
				{
					if (lines[i].Contains(invalid))
						lines[i] = lines[i].Replace(invalid, replacementForInvalid);
				}
			}
		}
	}
}
