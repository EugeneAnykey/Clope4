﻿using System.Windows.Forms;

namespace ClopeWin
{
	public static class WinHelper
	{
		public static void SetValues(this NumericUpDown numeric, decimal min, decimal value, decimal max)
		{
			numeric.ToggleEnabled(false);
			//numeric.BeginInit();

			numeric.Minimum = min;
			numeric.Maximum = max;
			numeric.Value = value;

			//numeric.EndInit();
			numeric.ToggleEnabled(true);
		}

		static void ToggleEnabled(this Control c, bool state)
		{
			c.Enabled = state;
		}

		public static void SetValues(this ScrollBar scrollBar, int min, int value, int max)
		{
			//scrollBar.ToggleEnabled(false);
			//scrollBar.SuspendLayout();

			scrollBar.Minimum = min;
			scrollBar.Maximum = max;
			scrollBar.Value = value;

			//scrollBar.ResumeLayout();
			//scrollBar.ToggleEnabled(true);
		}

		public static void ShowInformation(string text, string caption)
		{
			MessageBox.Show(
				text,
				caption,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}
	}
}
