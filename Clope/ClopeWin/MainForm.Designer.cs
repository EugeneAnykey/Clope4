﻿/*
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
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1Result;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.GroupBox groupBoxResults;
		private System.Windows.Forms.ListBox listBoxResults;
		
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
			this.groupBoxResults = new System.Windows.Forms.GroupBox();
			this.listBoxResults = new System.Windows.Forms.ListBox();
			this.groupBoxControl = new System.Windows.Forms.GroupBox();
			this.buttonClopeRun = new System.Windows.Forms.Button();
			this.groupBoxSettings = new System.Windows.Forms.GroupBox();
			this.dataSetupControl1 = new ClopeWin.DataSetupControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1Result = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.tabPage3Logger = new System.Windows.Forms.TabPage();
			this.richTextBoxLogger = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBoxResults.SuspendLayout();
			this.groupBoxControl.SuspendLayout();
			this.groupBoxSettings.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1Result.SuspendLayout();
			this.tabPage3Logger.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel2MinSize = 100;
			this.splitContainer1.Size = new System.Drawing.Size(940, 608);
			this.splitContainer1.SplitterDistance = 313;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBoxResults);
			this.panel1.Controls.Add(this.groupBoxControl);
			this.panel1.Controls.Add(this.groupBoxSettings);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(313, 608);
			this.panel1.TabIndex = 1;
			// 
			// groupBoxResults
			// 
			this.groupBoxResults.Controls.Add(this.listBoxResults);
			this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxResults.Location = new System.Drawing.Point(5, 314);
			this.groupBoxResults.Name = "groupBoxResults";
			this.groupBoxResults.Padding = new System.Windows.Forms.Padding(10);
			this.groupBoxResults.Size = new System.Drawing.Size(303, 289);
			this.groupBoxResults.TabIndex = 4;
			this.groupBoxResults.TabStop = false;
			this.groupBoxResults.Text = "groupBox1";
			// 
			// listBoxResults
			// 
			this.listBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxResults.FormattingEnabled = true;
			this.listBoxResults.Location = new System.Drawing.Point(10, 23);
			this.listBoxResults.Name = "listBoxResults";
			this.listBoxResults.Size = new System.Drawing.Size(283, 256);
			this.listBoxResults.TabIndex = 4;
			// 
			// groupBoxControl
			// 
			this.groupBoxControl.Controls.Add(this.buttonClopeRun);
			this.groupBoxControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxControl.Location = new System.Drawing.Point(5, 259);
			this.groupBoxControl.Name = "groupBoxControl";
			this.groupBoxControl.Size = new System.Drawing.Size(303, 55);
			this.groupBoxControl.TabIndex = 5;
			this.groupBoxControl.TabStop = false;
			this.groupBoxControl.Text = "Control";
			// 
			// buttonClopeRun
			// 
			this.buttonClopeRun.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonClopeRun.Location = new System.Drawing.Point(3, 29);
			this.buttonClopeRun.Name = "buttonClopeRun";
			this.buttonClopeRun.Size = new System.Drawing.Size(297, 23);
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
			this.groupBoxSettings.Size = new System.Drawing.Size(303, 254);
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
			this.dataSetupControl1.Size = new System.Drawing.Size(283, 221);
			this.dataSetupControl1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1Result);
			this.tabControl1.Controls.Add(this.tabPage3Logger);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(623, 608);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1Result
			// 
			this.tabPage1Result.Controls.Add(this.richTextBox1);
			this.tabPage1Result.Location = new System.Drawing.Point(4, 22);
			this.tabPage1Result.Name = "tabPage1Result";
			this.tabPage1Result.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1Result.Size = new System.Drawing.Size(615, 582);
			this.tabPage1Result.TabIndex = 0;
			this.tabPage1Result.Text = "Result";
			this.tabPage1Result.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(609, 576);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// tabPage3Logger
			// 
			this.tabPage3Logger.Controls.Add(this.richTextBoxLogger);
			this.tabPage3Logger.Location = new System.Drawing.Point(4, 22);
			this.tabPage3Logger.Name = "tabPage3Logger";
			this.tabPage3Logger.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3Logger.Size = new System.Drawing.Size(615, 582);
			this.tabPage3Logger.TabIndex = 2;
			this.tabPage3Logger.Text = "Logger";
			this.tabPage3Logger.UseVisualStyleBackColor = true;
			// 
			// richTextBoxLogger
			// 
			this.richTextBoxLogger.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxLogger.Location = new System.Drawing.Point(3, 3);
			this.richTextBoxLogger.Name = "richTextBoxLogger";
			this.richTextBoxLogger.Size = new System.Drawing.Size(609, 576);
			this.richTextBoxLogger.TabIndex = 0;
			this.richTextBoxLogger.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(940, 608);
			this.Controls.Add(this.splitContainer1);
			this.KeyPreview = true;
			this.MaximumSize = new System.Drawing.Size(1366, 768);
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.Text = "ClopeWin";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBoxResults.ResumeLayout(false);
			this.groupBoxControl.ResumeLayout(false);
			this.groupBoxSettings.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1Result.ResumeLayout(false);
			this.tabPage3Logger.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.GroupBox groupBoxControl;
		private System.Windows.Forms.Button buttonClopeRun;
		private DataSetupControl dataSetupControl1;
		private System.Windows.Forms.TabPage tabPage3Logger;
		private System.Windows.Forms.RichTextBox richTextBoxLogger;
	}
}
