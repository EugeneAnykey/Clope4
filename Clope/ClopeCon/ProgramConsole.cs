using System;
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

			if (fileParams.Pause)
				Console.ReadKey();
		}



		static bool IsParamsGood()
		{
			if (System.IO.File.Exists(fileParams.Filename))
				return true;
			else
			{
				Console.WriteLine("Bad params.\n");
				Console.WriteLine(FileParams.GetHelp());
				Console.ReadKey();
			}

			return false;
		}
	}
}
