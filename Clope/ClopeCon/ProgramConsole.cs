using EugeneAnykey.DebugLib.Loggers;
using System;

namespace ClopeCon
{
	class ProgramConsole
	{
		static FileParams fileParams;

		// main
		static void Main(string[] args)
		{
			fileParams.DefaultTest();

			ParseArgs(args);

			Tester tester = new Tester(fileParams, new FileLogger("log.txt", new ConsoleLogger()));
			tester.Run();

			Console.WriteLine(tester.MakeResults(fileParams.ColumnToView));

			Console.ReadKey();
		}



		static string SeparateParam(string input) => input.Substring(input.IndexOf("=") + 1);

		static void ParseArgs(string[] args)
		{
			const string paramNameRepulsion = "repulsion";
			const string paramNameFile = "file";
			const string paramNameColumn = "col";
			const string paramNameSeparator = "separator";
			const string paramNameSkipLines = "skip";

			Console.WriteLine("Command line parameters:");

			foreach (var s in args)
			{
				Console.WriteLine(s);

				if (s.Contains(paramNameRepulsion))
				{
					if (float.TryParse(SeparateParam(s), out float val))
						fileParams.Repulsion = val;
				}

				else if (s.Contains(paramNameColumn))
				{
					if (int.TryParse(SeparateParam(s), out int val))
						fileParams.ColumnToView = val;
				}

				else if (s.Contains(paramNameFile))
				{
					fileParams.Filename = SeparateParam(s);
				}

				else if (s.Contains(paramNameSeparator))
				{
					char sep = ';';
					var val = SeparateParam(s);
					if (val == "\t")
						sep = '\t';
					else if (string.IsNullOrEmpty(val))
						sep = ';';
					else sep = val[0];

					fileParams.Separator = sep;
				}

				else if (s.Contains(paramNameSkipLines))
				{
					if (int.TryParse(SeparateParam(s), out int val))
						fileParams.FirstLinesToSkip = val;
				}
			}
		}
	}
}
