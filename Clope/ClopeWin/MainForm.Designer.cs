/*
 * ClopeWin - ${ClassName}
 * file versions: 2019-02-14.
 * Eugene Anykey (c).
 */
namespace ClopeWin
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBoxSettings;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBoxControl = new System.Windows.Forms.GroupBox();
			this.buttonScreenShot = new System.Windows.Forms.Button();
			this.buttonClopeRun = new System.Windows.Forms.Button();
			this.groupBoxSettings = new System.Windows.Forms.GroupBox();
			this.dataSetupControl1 = new ClopeWin.DataSetupControl();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.richTextBoxClusters = new System.Windows.Forms.RichTextBox();
			this.richTextBoxLogger = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBoxControl.SuspendLayout();
			this.groupBoxSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			this.splitContainer1.Panel1MinSize = 100;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel2MinSize = 100;
			this.splitContainer1.Size = new System.Drawing.Size(1184, 812);
			this.splitContainer1.SplitterDistance = 334;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBoxControl);
			this.panel1.Controls.Add(this.groupBoxSettings);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(334, 812);
			this.panel1.TabIndex = 1;
			// 
			// groupBoxControl
			// 
			this.groupBoxControl.Controls.Add(this.buttonScreenShot);
			this.groupBoxControl.Controls.Add(this.buttonClopeRun);
			this.groupBoxControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxControl.Location = new System.Drawing.Point(5, 215);
			this.groupBoxControl.Name = "groupBoxControl";
			this.groupBoxControl.Size = new System.Drawing.Size(324, 74);
			this.groupBoxControl.TabIndex = 5;
			this.groupBoxControl.TabStop = false;
			this.groupBoxControl.Text = "Control";
			// 
			// buttonScreenShot
			// 
			this.buttonScreenShot.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonScreenShot.Location = new System.Drawing.Point(3, 48);
			this.buttonScreenShot.Name = "buttonScreenShot";
			this.buttonScreenShot.Size = new System.Drawing.Size(318, 23);
			this.buttonScreenShot.TabIndex = 6;
			this.buttonScreenShot.Text = "Make Screenshot";
			this.buttonScreenShot.UseVisualStyleBackColor = true;
			// 
			// buttonClopeRun
			// 
			this.buttonClopeRun.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonClopeRun.Location = new System.Drawing.Point(3, 16);
			this.buttonClopeRun.Name = "buttonClopeRun";
			this.buttonClopeRun.Size = new System.Drawing.Size(318, 23);
			this.buttonClopeRun.TabIndex = 5;
			this.buttonClopeRun.Text = "Run";
			this.buttonClopeRun.UseVisualStyleBackColor = true;
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Controls.Add(this.dataSetupControl1);
			this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxSettings.Location = new System.Drawing.Point(5, 5);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Padding = new System.Windows.Forms.Padding(10);
			this.groupBoxSettings.Size = new System.Drawing.Size(324, 210);
			this.groupBoxSettings.TabIndex = 2;
			this.groupBoxSettings.TabStop = false;
			this.groupBoxSettings.Text = "Settings";
			// 
			// dataSetupControl1
			// 
			this.dataSetupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataSetupControl1.Location = new System.Drawing.Point(10, 23);
			this.dataSetupControl1.Name = "dataSetupControl1";
			this.dataSetupControl1.Padding = new System.Windows.Forms.Padding(5);
			this.dataSetupControl1.Size = new System.Drawing.Size(304, 177);
			this.dataSetupControl1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.richTextBoxClusters);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.richTextBoxLogger);
			this.splitContainer2.Size = new System.Drawing.Size(846, 812);
			this.splitContainer2.SplitterDistance = 291;
			this.splitContainer2.TabIndex = 3;
			// 
			// richTextBox1
			// 
			this.richTextBoxClusters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxClusters.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxClusters.Name = "richTextBox1";
			this.richTextBoxClusters.Size = new System.Drawing.Size(291, 812);
			this.richTextBoxClusters.TabIndex = 2;
			this.richTextBoxClusters.Text = "";
			// 
			// richTextBoxLogger
			// 
			this.richTextBoxLogger.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxLogger.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxLogger.Name = "richTextBoxLogger";
			this.richTextBoxLogger.Size = new System.Drawing.Size(551, 812);
			this.richTextBoxLogger.TabIndex = 1;
			this.richTextBoxLogger.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 812);
			this.Controls.Add(this.splitContainer1);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ClopeWin";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBoxControl.ResumeLayout(false);
			this.groupBoxSettings.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.GroupBox groupBoxControl;
		private System.Windows.Forms.Button buttonClopeRun;
		private DataSetupControl dataSetupControl1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RichTextBox richTextBoxClusters;
		private System.Windows.Forms.RichTextBox richTextBoxLogger;
		private System.Windows.Forms.Button buttonScreenShot;
	}
}
