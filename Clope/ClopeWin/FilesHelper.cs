using System.Collections.Generic;
using TestingHelper;

namespace ClopeWin
{
	public static class FilesHelper
	{
		// field:
		public static FileBroker FileBroker { get; }
		public static List<DelimitedFile> Files { get; }



		// init
		static FilesHelper()
		{
			var commaSep = new[] { ',' };
			var commaSpaceSep = new[] { ',', ' ' };
			var commaTabSep = new[] { ',', '\t' };
			var tabSep = new[] { '\t' };

			Files = new List<DelimitedFile>();
			Files.AddRange(
				new DelimitedFile[] {
					new DelimitedFile("empty.txt", commaTabSep),
					new DelimitedFile("agaricus-lepiota.csv", commaSep),
					new DelimitedFile("mushrooms.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("megapolis_clean_quanted.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("ex_article.txt", commaSep),
					new DelimitedFile("ex_a1.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("ex_a2.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("ex_a3.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("lines_01.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("lines_02.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("megapolis_clean_approx_1.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("agaricus-lepiota.safe", commaSep),
					new DelimitedFile("agaricus-lepiota.small", commaSep),
				}
			);

			FileBroker = new FileBroker(@"..\..\..\..\data\");

			foreach (var file in Files)
			{
				file.Broker = FileBroker;
			}
		}
	}
}
