using System;

namespace EugeneAnykey.DebugLib.Loggers
{
	public abstract class BaseLogger
	{
		readonly protected ILogger linkedLogger;
		public ILogger LinkedLogger => LinkedLogger;

		public bool UseLocalTime { get; set; } = true;

		protected string Time => (UseLocalTime ? DateTime.Now : DateTime.UtcNow).ToLongTimeString();



		public BaseLogger(ILogger linked = null)
		{
			linkedLogger = linked;
		}
	}
}
