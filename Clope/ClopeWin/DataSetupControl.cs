using System.IO;
using System.Windows.Forms;
using TestingHelper;

namespace ClopeWin
{
	public partial class DataSetupControl : UserControl
	{
		// field
		DataSetupSettings settings;
		public DataSetupSettings Settings { get => settings; }



		// init
		public DataSetupControl()
		{
			InitializeComponent();

			listBoxFiles.SelectedIndexChanged += (_, __) => SelectingFile();
			repulsionScrollBar.ValueChanged += (_, __) => ChangeRepulsion();

			settings.SelectedDelimitedFile = null;
			repulsionScrollBar.SetValues(10, 27, 100);

			listBoxFiles.Items.AddRange(FilesHelper.Files.ToArray());
			listBoxFiles.SelectedIndex = 1;
		}



		// private
		void SelectingFile()
		{
			settings.SelectedDelimitedFile = listBoxFiles.SelectedItem as DelimitedFile;

			if (!File.Exists(settings.SelectedDelimitedFile.GetPath()))
				WinHelper.ShowInformation($"File <{settings.SelectedDelimitedFile.Filename}> is missing.", "File not found");
		}

		void ChangeRepulsion()
		{
			settings.ClopeRepulsion = repulsionScrollBar.Value / 10.0f;
			labelRepulsion.Text = $"Repulsion: {settings.ClopeRepulsion}.";
		}
	}
}
