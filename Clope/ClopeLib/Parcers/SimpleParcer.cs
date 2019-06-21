using System;

namespace ClopeLib.Parcers
{
	public class SimpleParcer : IParcer
	{
		// const
		public static readonly char[] CommaSplitter = { ',' };
		public static readonly char[] CommaSpaceSplitter = { ',', ' ' };
		public static readonly char[] SemicolonSplitter = { ';' };
		public static readonly char[] SemicolonSpaceSplitter = { ';', ' ' };

		// field
		public char[] Splitter { get; set; }



		// init
		public SimpleParcer() : this(CommaSplitter) { }

		public SimpleParcer(char[] splitter)
		{
			Splitter = splitter;
		}



		//  public
		public string[] Parce(string line)
		{
			var result = line.Split(Splitter, StringSplitOptions.RemoveEmptyEntries);

			var invalids = new[] { "?" };

			InvalidDataCorrection(result, invalids, null);

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
