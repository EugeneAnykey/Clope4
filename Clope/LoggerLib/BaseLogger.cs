using System;

namespace EugeneAnykey.DebugLib.Loggers
{
	public abstract class BaseLogger
	{
		readonly protected ILogger linkedLogger;
		public ILogger LinkedLogger => LinkedLogger;

		protected string Time => DateTime.UtcNow.ToLongTimeString();

		public BaseLogger(ILogger linked = null)
		{
			linkedLogger = linked;
		}
	}
}
