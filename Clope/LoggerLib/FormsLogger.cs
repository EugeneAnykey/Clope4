using System;
using System.Windows.Forms;

namespace EugeneAnykey.DebugLib.Loggers
{
	public class FormsLogger : BaseLogger, ILogger
	{
		// field
		readonly RichTextBox textBox;



		// init
		public FormsLogger(RichTextBox textBox, ILogger linked = null) : base(linked)
		{
			this.textBox = textBox ?? throw new ArgumentNullException();
		}



		// ILogger
		public void Write(string message)
		{
			textBox.AppendText($"{message}\n");
			textBox.ScrollToCaret();
			linkedLogger?.Write(message);
		}

		public void WriteDated(string message)
		{
			textBox.AppendText($"{Time}: {message}\n");
			textBox.ScrollToCaret();
			linkedLogger?.WriteDated(message);
		}
	}
}
