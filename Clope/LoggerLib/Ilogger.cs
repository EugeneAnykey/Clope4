namespace EugeneAnykey.DebugLib.Loggers
{
	public interface ILogger
	{
		ILogger LinkedLogger { get; }

		void Write(string message);

		void WriteDated(string message);
	}
}
