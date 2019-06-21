using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ClopeLib.Algo;
using ClopeLib.Readers;
using EugeneAnykey.DebugLib.Loggers;
using TestingHelper;

namespace ClopeWin
{
	public partial class MainForm : Form
	{
		// field
		ILogger logger;
		Clope clope = new Clope();



		// init
		public MainForm()
		{
			InitializeComponent();

			logger = new FormsLogger(richTextBoxLogger, new FileLogger("clope.log.txt"));
			tabControl1.SelectedIndex = 1;

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
			clope.Clear();

			Tester tester = new Tester(clope, dataSetupControl1.Settings, logger);

			tester.Run();

			richTextBox1.AppendText(tester.MakeResults());

			listBoxResults.SelectedIndex = listBoxResults.Items.Count - 1;
			ShowInfo();
		}
		#endregion
	}
}
