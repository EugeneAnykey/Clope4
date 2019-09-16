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

			Console.WriteLine(tester.MakeResults());

			Console.ReadKey();
		}



		static void ParseArgs(string[] args)
		{
			const string paramRepulsionName = "repulsion";

			foreach (var s in args)
			{
				if (s.Contains(paramRepulsionName))
				{
					// repulsion
				}
			}
		}
	}
}
