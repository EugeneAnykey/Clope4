using System.Windows.Forms;

using ClopeLib.Algo;
using ClopeLib.Algo4;
using EugeneAnykey.DebugLib.Loggers;

namespace ClopeWin
{
	public partial class MainForm : Form
	{
		// field
		ILogger logger;
		Clope clope;
		Clope4 clope4;



		// init
		public MainForm()
		{
			InitializeComponent();

			logger = new FormsLogger(richTextBoxLogger, new FileLogger("clope.log.txt"));
			tabControl1.SelectedIndex = 1;

			clope = new Clope();
			clope4 = new Clope4();

			// events:
			buttonClopeRun.Click += (_, __) => RunClope();
			listBoxResults.SelectedIndexChanged += (_, __) => ShowInfo();
			clope.StepDone += (_, __) => StepDone();
		}



		#region Clope.
		void ShowInfo()
		{
			ShowInfo(listBoxResults.SelectedItem as ClopeStepResult);
		}

		void ShowInfo(ClopeStepResult res)
		{
			richTextBoxOutput.Text = res.Output;
			richTextBoxInfo.Text = res.Info;
		}

		void StepDone()
		{
			if (clope == null)
				return;

			var csr = new ClopeStepResult(
				clope.CurrentStepName,
				clope.CurrentOutput(),
				clope.CurrentInfo()
			);

			listBoxResults.Items.Add(csr);
		}



		void RunClope()
		{
			listBoxResults.Items.Clear();
			richTextBox1.Clear();
			clope?.Clear();
			clope4.Clear();

			if (clope != null)
			{
				var tester = new Tester(clope, dataSetupControl1.Settings, logger);
				tester.Run();
				richTextBox1.AppendText(tester.MakeResults());
			}

			if (clope4 != null)
			{
				var tester4 = new Tester4(clope4, dataSetupControl1.Settings, logger);
				tester4.Run();
				richTextBox1.AppendText(tester4.MakeResults());
			}

			listBoxResults.SelectedIndex = listBoxResults.Items.Count - 1;
			ShowInfo();
		}
		#endregion
	}
}
