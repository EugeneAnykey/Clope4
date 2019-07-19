using System.Drawing;
using System.Drawing.Imaging;
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

		public static void TakeComponentScreenShot(Control control, string filename)
		{
			// find absolute position of the control in the screen.
			Control ctrl = control;
			Rectangle rect = new Rectangle(Point.Empty, ctrl.Size);
			do
			{
				rect.Offset(ctrl.Location);
				ctrl = ctrl.Parent;
			} while (ctrl != null);

			using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb))
			{
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
				}
				bitmap.Save(filename, ImageFormat.Png);
			}
		}



		public static string Replace(this string str, string[] oldValues, string newValue)
		{
			foreach (var item in oldValues)
				str = str.Replace(item, newValue);
			return str;
		}
	}
}
