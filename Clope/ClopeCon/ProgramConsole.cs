using System;
using System.IO;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeCon
{
	class ProgramConsole
	{
		static FileParams fileParams;

		// main
		static void Main(string[] args)
		{
			fileParams.ParseArgs(args);

			if (IsParamsGood())
			{
				Tester tester = new Tester(fileParams, new FileLogger("log.txt", new ConsoleLogger()));
				tester.Run();

				Console.WriteLine(tester.MakeResults(fileParams.ColumnToView));
			}

			Console.ReadKey();
		}



		static bool IsParamsGood()
		{
			if (File.Exists(fileParams.Filename))
				return true;
			else
			{
				Console.WriteLine("Bad params.\n");
				Console.WriteLine(FileParams.GetHelp());
			}

			return false;
		}
	}
}
