using EugeneAnykey.DebugLib.Loggers;

namespace SpeedTests
{
	class SpeedTestsProgram
	{
		static void Main(string[] args)
		{
			//RunPowerCachingSpeedTest();

			RunHoldersSpeedTest();

			System.Console.WriteLine();
			System.Console.WriteLine("Speed testing finished.");
			System.Console.WriteLine("Press any key to exit. . .");
			System.Console.ReadKey();
		}



		static void RunPowerCachingSpeedTest()
		{
			const string logFilename = "speed-power_caching.txt";
			ILogger logger = new FileLogger(logFilename, new ConsoleLogger());

			new PowerCachingTest(logger).Run();
		}



		static void RunHoldersSpeedTest()
		{
			// iitem	dic	list
			// class	x	v
			// struct	v	v
			const string logFilename = "speed-holders.txt";
			ILogger logger = new FileLogger(logFilename, new ConsoleLogger());

			new HoldersSpeedTest(logger).Run();
		}
	}
}
