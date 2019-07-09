using ClopeLib.Algo;
using ClopeWin;
using EugeneAnykey.DebugLib.Loggers;
using System;

namespace ClopeConsole
{
	class ConsoleProgram
	{
		// field
		static ILogger logger;



		// main
		static void Main(string[] args)
		{
			logger = new ConsoleLogger(new FileLogger("clope.log.txt"));

			RunClope(2.7f);
			RunClope(4.7f);

			Console.ReadKey();
		}



		static void RunClope(float repulsion)
		{
			const string end = "\r\n\r\n";

			var clope4 = new Clope4();

			var settings = new DataSetupSettings
			{
				ClopeRepulsion = repulsion,
				SelectedDelimitedFile = FilesHelper.File(1)
			};

			var tester4 = new Tester4(clope4, settings, logger);
			tester4.Run();
			Console.WriteLine(tester4.MakeResults());

			logger.Write(end);
		}
	}
}
