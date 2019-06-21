using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ClopeLib.Readers;
using TestingHelper;

namespace ClopeWin
{
	public partial class DataSetupControl : UserControl
	{
		// field
		DataSetupSettings settings;
		public DataSetupSettings Settings { get => settings; }

		#region init
		public DataSetupControl()
		{
			InitializeComponent();

			InitEvent();
			Init();

			listBoxFiles.Items.AddRange(FilesHelper.Files.ToArray());
			listBoxFiles.SelectedIndex = 0;
		}

		void InitEvent()
		{
			listBoxFiles.SelectedIndexChanged += (_, __) => SelectingFile();

			repulsionScrollBar.ValueChanged += (_, __) => ChangeRepulsion();
			numericNullColumn.ValueChanged += (_, __) => settings.NullColumn = (int)numericNullColumn.Value;
			numericNullJumps.ValueChanged += (_, __) => settings.NullJumps = (int)numericNullJumps.Value;
		}

		void Init()
		{
			settings.SelectedDelimitedFile = null;
			
			repulsionScrollBar.SetValues(10, 25, 100);
			numericNullColumn.SetValues(0, 0, 100);
			numericNullJumps.SetValues(0, 0, 100);
		}
		#endregion



		// private
		void SelectingFile()
		{
			settings.SelectedDelimitedFile = listBoxFiles.SelectedItem as DelimitedFile;
			ExamineFile(settings.SelectedDelimitedFile?.GetPath());
		}



		void ExamineFile(string filepath)
		{
			const int examineLines = 5;

			if (filepath == null)
				throw new ArgumentNullException();

			List<string> lines = null;

			if (!File.Exists(filepath))
			{
				WinHelper.ShowInformation($"File <{Path.GetFileName(filepath)}> does not exists", "No file");
			}
			else
			{
				using (var reader = new Reader(filepath))
				{
					lines = reader.GetData(examineLines);
				}
			}

			bool gotLines = lines != null && lines.Count > 0;

			numericNullColumn.Enabled = gotLines;
			numericNullJumps.Enabled = gotLines;

			if (gotLines)
			{
				var ss = lines[0].Split(settings.SelectedDelimitedFile.FieldSeparators);
				numericNullColumn.SetValues(0, 0, ss.Length);
			}

			numericNullColumn.Value = 0;
			numericNullJumps.Value = 0;
		}



		void ChangeRepulsion()
		{
			settings.ClopeRepulsion = repulsionScrollBar.Value / 10.0f;
			labelRepulsion.Text = $"Repulsion: {settings.ClopeRepulsion}.";
		}

	}
}
