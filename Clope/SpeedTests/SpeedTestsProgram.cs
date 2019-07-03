using EugeneAnykey.DebugLib.Loggers;
using System;
using System.Linq;

namespace SpeedTests
{
	class SpeedTestsProgram
	{
		static void Main(string[] args)
		{
			RunPowerCachingTest();
		}



		static void RunPowerCachingTest()
		{
			const string logFilename = "speed-power_caching.txt";
			ILogger logger = new FileLogger(logFilename, new ConsoleLogger());

			new PowerCachingTest(logger).Run();
		}
	}
}
