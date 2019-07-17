using System;
using System.Linq;
using System.Windows.Forms;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeWin
{
	public partial class MainForm : Form
	{
		// field
		ILogger logger;
		Tester tester = null;



		// init
		public MainForm()
		{
			InitializeComponent();

			logger = new FormsLogger(richTextBoxLogger, new FileLogger("clope.log.txt"));

			ShowOptimizations();

			// events:
			buttonRunTest.Click += (_, __) => RunTest();
			buttonScreenShot.Click += (_, __) => MakeScreenshot(richTextBoxLogger);
			numericUpDownResultColumn.ValueChanged += (_, __) => Results((int)numericUpDownResultColumn.Value);
		}



		readonly string[] replacements = new[] { ".", "/", "\\", ":" };
		const string goodDelimeter = "-";

		void MakeScreenshot(Control control)
		{
			WinHelper.TakeComponentScreenShot(
				control,
				string.Concat(DateTime.Now.ToShortDateString(), " - ", DateTime.Now.ToLongTimeString()).Replace(replacements, goodDelimeter)
			);
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
				"only external transactions",
				""
			};

			var list = optimizations.Select(s => "\t" + s);

			logger.Write("Optimizations done:");
			logger.Write(string.Join(sep, list));
		}



		void RunTest()
		{
			tester = new Tester(dataSetupControl1.Settings, logger);
			tester.Run();
			Results();

			logger.Write();
			logger.Write();
		}


		void Results(int columnIndex = 0) => richTextBoxClusters.Text = tester.MakeResults(columnIndex);
	}
}
