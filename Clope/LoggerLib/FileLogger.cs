using System;
using System.IO;

namespace EugeneAnykey.DebugLib.Loggers
{
	public class FileLogger : BaseLogger, ILogger
	{
		readonly string filename;

		public FileLogger(string filename, ILogger linked = null) : base(linked)
		{
			if (string.IsNullOrEmpty(filename))
				throw new Exception("Filename should be setted");

			this.filename = filename;
		}



		// ILogger
		public void Write(string message)
		{
			File.AppendAllText(filename, $"{message}\r\n");
			linkedLogger?.Write(message);
		}

		public void WriteDated(string message)
		{
			File.AppendAllText(filename, $"{Time}: {message}\r\n");
			linkedLogger?.WriteDated(message);
		}
	}
}
