using System.Collections.Generic;
using TestingHelper;

namespace ClopeWin
{
	public static class FilesHelper
	{
		// field:
		public static FileBroker FileBroker { get; }
		public static List<DelimitedFile> Files { get; }



		public static DelimitedFile File(int index) => (0 <= index && index < Files.Count) ? Files[index] : Files[0];



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
					new DelimitedFile("mushrooms.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("agaricus-lepiota.csv", commaSep),
					new DelimitedFile("ex_simple.txt", tabSep) { FirstLinesToSkip = 1 },
					new DelimitedFile("ex_duplicates.txt", tabSep) { FirstLinesToSkip = 1 },
				}
			);

			FileBroker = new FileBroker(@"..\..\..\..\data\");
			//FileBroker = new FileBroker(@".\data\");

			foreach (var file in Files)
			{
				file.Broker = FileBroker;
			}
		}
	}
}
