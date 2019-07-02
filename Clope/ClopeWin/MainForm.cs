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

			groupBoxResults.Visible = false;
		}



		void RunClope()
		{
			const string end = "\r\n\r\n";

			listBoxResults.Items.Clear();
			richTextBox1.Clear();

			clope4 = new Clope4();

			if (clope4 != null)
			{
				clope4.Clear();
				var tester4 = new Tester4(clope4, dataSetupControl1.Settings, logger);
				tester4.Run();
				richTextBox1.AppendText(tester4.MakeResults());

				logger.Write(end);
			}

			listBoxResults.SelectedIndex = listBoxResults.Items.Count - 1;
		}
	}
}
