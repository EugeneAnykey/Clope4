using System.IO;
using System.Linq;

namespace TestingHelper.UnitTests
{
	public class Outputer : IOutputer
	{
		// field
		readonly string filename;

		//init
		public Outputer(string filename)
		{
			this.filename = filename;
		}

		// interface
		public void Output<T>(T[] items)
		{
			if (items is HasherTestItem[])
				Output(items as HasherTestItem[]);
		}



		// misc
		public void Output(HasherTestItem[] items)
		{
			Output(items.Select(h => $"{h.Id}\t{h.Line}\t{h.Hash}").ToArray());
		}

		public void Output(string[] lines)
		{
			File.WriteAllLines(filename, lines);
		}
	}
}
