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
			this.numericNullColumn = new System.Windows.Forms.NumericUpDown();
			this.numericNullJumps = new System.Windows.Forms.NumericUpDown();
			this.LabelNullJumps = new System.Windows.Forms.Label();
			this.labelNullColumn = new System.Windows.Forms.Label();
			this.listBoxFiles = new System.Windows.Forms.ListBox();
			this.labelRepulsion = new System.Windows.Forms.Label();
			this.repulsionScrollBar = new System.Windows.Forms.HScrollBar();
			((System.ComponentModel.ISupportInitialize)(this.numericNullColumn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericNullJumps)).BeginInit();
			this.SuspendLayout();
			// 
			// numericNullColumn
			// 
			this.numericNullColumn.Location = new System.Drawing.Point(101, 66);
			this.numericNullColumn.Name = "numericNullColumn";
			this.numericNullColumn.Size = new System.Drawing.Size(65, 20);
			this.numericNullColumn.TabIndex = 17;
			this.numericNullColumn.Visible = false;
			// 
			// numericNullJumps
			// 
			this.numericNullJumps.Location = new System.Drawing.Point(101, 91);
			this.numericNullJumps.Name = "numericNullJumps";
			this.numericNullJumps.Size = new System.Drawing.Size(65, 20);
			this.numericNullJumps.TabIndex = 16;
			this.numericNullJumps.Visible = false;
			// 
			// LabelNullJumps
			// 
			this.LabelNullJumps.AutoSize = true;
			this.LabelNullJumps.Location = new System.Drawing.Point(7, 93);
			this.LabelNullJumps.Name = "LabelNullJumps";
			this.LabelNullJumps.Size = new System.Drawing.Size(61, 13);
			this.LabelNullJumps.TabIndex = 15;
			this.LabelNullJumps.Text = "Null Jumps:";
			this.LabelNullJumps.Visible = false;
			// 
			// labelNullColumn
			// 
			this.labelNullColumn.AutoSize = true;
			this.labelNullColumn.Location = new System.Drawing.Point(7, 68);
			this.labelNullColumn.Name = "labelNullColumn";
			this.labelNullColumn.Size = new System.Drawing.Size(88, 13);
			this.labelNullColumn.TabIndex = 14;
			this.labelNullColumn.Text = "Add Null Column:";
			this.labelNullColumn.Visible = false;
			// 
			// listBoxFiles
			// 
			this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listBoxFiles.FormattingEnabled = true;
			this.listBoxFiles.Location = new System.Drawing.Point(5, 123);
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
			this.Controls.Add(this.numericNullColumn);
			this.Controls.Add(this.numericNullJumps);
			this.Controls.Add(this.LabelNullJumps);
			this.Controls.Add(this.labelNullColumn);
			this.Controls.Add(this.listBoxFiles);
			this.Controls.Add(this.labelRepulsion);
			this.Controls.Add(this.repulsionScrollBar);
			this.Name = "DataSetupControl";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(447, 223);
			((System.ComponentModel.ISupportInitialize)(this.numericNullColumn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericNullJumps)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericNullColumn;
		private System.Windows.Forms.NumericUpDown numericNullJumps;
		private System.Windows.Forms.Label LabelNullJumps;
		private System.Windows.Forms.Label labelNullColumn;
		private System.Windows.Forms.ListBox listBoxFiles;
		private System.Windows.Forms.Label labelRepulsion;
		private System.Windows.Forms.HScrollBar repulsionScrollBar;
	}
}
