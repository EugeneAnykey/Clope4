using System.Windows.Forms;

namespace ClopeWin
{
	public static class WinHelper
	{
		public static void SetValues(this NumericUpDown numeric, decimal min, decimal value, decimal max)
		{
			numeric.Enabled = false;

			numeric.Minimum = min;
			numeric.Maximum = max;
			numeric.Value = value;

			numeric.Enabled = true;
		}

		public static void SetValues(this ScrollBar scrollBar, int min, int value, int max)
		{
			scrollBar.Minimum = min;
			scrollBar.Maximum = max;
			scrollBar.Value = value;
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
