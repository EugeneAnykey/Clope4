using System;
using System.Linq;
using System.Windows.Forms;
using ClopeLib.Algo;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeWin
{
	public partial class MainForm : Form
	{
		// field
		ILogger logger;
		Clope4 clope4;



		// init
		public MainForm()
		{
			InitializeComponent();

			logger = new FormsLogger(richTextBoxLogger, new FileLogger("clope.log.txt"));

			// events:
			buttonClopeRun.Click += (_, __) => RunClope();
			buttonScreenShot.Click += (_, __) => MakeScreenshot(richTextBoxLogger);
		}



		void MakeScreenshot(Control control)
		{
			var name = string.Concat(DateTime.Now.ToShortDateString(), " - ", DateTime.Now.ToLongTimeString()).Replace(".", "-").Replace("/", "-").Replace(@"\", "-").Replace(":", "-") + ".png";

			WinHelper.TakeComponentScreenShot(control, name);
		}



		void RunClope()
		{
			const string end = "\r\n\r\n";

			clope4 = new Clope4();

			richTextBoxClusters.Clear();
			var tester4 = new Tester4(clope4, dataSetupControl1.Settings, logger);
			tester4.Run();
			richTextBoxClusters.Text = tester4.MakeResults();

			logger.Write(end);
		}
	}
}
