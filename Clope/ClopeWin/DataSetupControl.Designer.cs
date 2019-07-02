namespace ClopeWin
{
	partial class DataSetupControl
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBoxFiles = new System.Windows.Forms.ListBox();
			this.labelRepulsion = new System.Windows.Forms.Label();
			this.repulsionScrollBar = new System.Windows.Forms.HScrollBar();
			this.SuspendLayout();
			// 
			// listBoxFiles
			// 
			this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listBoxFiles.FormattingEnabled = true;
			this.listBoxFiles.Location = new System.Drawing.Point(5, 78);
			this.listBoxFiles.Name = "listBoxFiles";
			this.listBoxFiles.Size = new System.Drawing.Size(437, 95);
			this.listBoxFiles.TabIndex = 13;
			// 
			// labelRepulsion
			// 
			this.labelRepulsion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRepulsion.Location = new System.Drawing.Point(7, 14);
			this.labelRepulsion.Name = "labelRepulsion";
			this.labelRepulsion.Size = new System.Drawing.Size(423, 23);
			this.labelRepulsion.TabIndex = 12;
			this.labelRepulsion.Text = "Clope repulsion: 1";
			// 
			// repulsionScrollBar
			// 
			this.repulsionScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.repulsionScrollBar.Location = new System.Drawing.Point(7, 37);
			this.repulsionScrollBar.Maximum = 109;
			this.repulsionScrollBar.Minimum = 10;
			this.repulsionScrollBar.Name = "repulsionScrollBar";
			this.repulsionScrollBar.Size = new System.Drawing.Size(423, 20);
			this.repulsionScrollBar.TabIndex = 11;
			this.repulsionScrollBar.Value = 10;
			// 
			// DataSetupControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listBoxFiles);
			this.Controls.Add(this.labelRepulsion);
			this.Controls.Add(this.repulsionScrollBar);
			this.Name = "DataSetupControl";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(447, 178);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListBox listBoxFiles;
		private System.Windows.Forms.Label labelRepulsion;
		private System.Windows.Forms.HScrollBar repulsionScrollBar;
	}
}
