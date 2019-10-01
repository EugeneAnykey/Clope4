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
			fileParams.DefaultTest();
			fileParams.ParseArgs(args);

			Tester tester = new Tester(fileParams, new FileLogger("log.txt", new ConsoleLogger()));
			tester.Run();

			Console.WriteLine(tester.MakeResults(fileParams.ColumnToView));

			Console.ReadKey();
		}
	}
}
