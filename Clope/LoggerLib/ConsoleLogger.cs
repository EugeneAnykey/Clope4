using System;

namespace EugeneAnykey.DebugLib.Loggers
{
	public class ConsoleLogger : BaseLogger, ILogger
	{
		public ConsoleLogger(ILogger linked = null) : base(linked) { }



		// ILogger
		public void Write(string message)
		{
			Console.WriteLine($"{message}\n");
			linkedLogger?.Write(message);
		}

		public void WriteDated(string message)
		{
			Console.WriteLine($"{Time}: {message}\n");
			linkedLogger?.WriteDated(message);
		}
	}
}
