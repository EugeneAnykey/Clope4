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
		Clope clope4;



		// init
		public MainForm()
		{
			InitializeComponent();

			logger = new FormsLogger(richTextBoxLogger, new FileLogger("clope.log.txt"));

			ShowOptimizations();

			// events:
			buttonClopeRun.Click += (_, __) => RunClope();
			buttonScreenShot.Click += (_, __) => MakeScreenshot(richTextBoxLogger);
		}



		void MakeScreenshot(Control control)
		{
			var name = string.Concat(DateTime.Now.ToShortDateString(), " - ", DateTime.Now.ToLongTimeString()).Replace(".", "-").Replace("/", "-").Replace(@"\", "-").Replace(":", "-") + ".png";

			WinHelper.TakeComponentScreenShot(control, name);
		}



		void ShowOptimizations()
		{
			const string sep = "\r\n";
			// optimizations:
			var optimizations = new[] {
				"attribute store (at dictionary, class)",
				"occurence (at array, counter class)",
				"math power (at array, class)",
				"remove cost (function)",
				"duplicates allowed",
				//"simultaneous load (0 step in load)",
				""
			};

			var list = optimizations.Select(s => "\t" + s);

			logger.Write("Optimizations done:");
			logger.Write(string.Join(sep, list));
		}



		void RunClope()
		{
			const string end = "\r\n\r\n";

			clope4 = new Clope();

			richTextBoxClusters.Clear();
			var tester4 = new Tester(clope4, dataSetupControl1.Settings, logger);
			tester4.Run();
			richTextBoxClusters.Text = tester4.MakeResults();

			logger.Write(end);
		}
	}
}
