using System.Windows.Forms;

using ClopeLib.Algo4;
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
			tabControl1.SelectedIndex = 1;

			clope4 = new Clope4();

			// events:
			buttonClopeRun.Click += (_, __) => RunClope();
		}



		void RunClope()
		{
			listBoxResults.Items.Clear();
			richTextBox1.Clear();
			clope4.Clear();

			if (clope4 != null)
			{
				var tester4 = new Tester4(clope4, dataSetupControl1.Settings, logger);
				tester4.Run();
				richTextBox1.AppendText(tester4.MakeResults());
			}

			listBoxResults.SelectedIndex = listBoxResults.Items.Count - 1;
		}
	}
}
