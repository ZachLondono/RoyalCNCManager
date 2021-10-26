
namespace RoyalCNCTracker {
	partial class MainForm {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.SingleProgramList = new System.Windows.Forms.ListBox();
			this.BarcodeInput = new System.Windows.Forms.TextBox();
			this.ProgramImageBox = new System.Windows.Forms.PictureBox();
			this.ClearBtn = new System.Windows.Forms.Button();
			this.LoadSingleBtn = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.ProgramImageBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// SingleProgramList
			// 
			this.SingleProgramList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.SingleProgramList.FormattingEnabled = true;
			this.SingleProgramList.ItemHeight = 15;
			this.SingleProgramList.Location = new System.Drawing.Point(13, 57);
			this.SingleProgramList.Name = "SingleProgramList";
			this.SingleProgramList.Size = new System.Drawing.Size(200, 259);
			this.SingleProgramList.TabIndex = 0;
			// 
			// BarcodeInput
			// 
			this.BarcodeInput.Location = new System.Drawing.Point(14, 27);
			this.BarcodeInput.Name = "BarcodeInput";
			this.BarcodeInput.Size = new System.Drawing.Size(199, 23);
			this.BarcodeInput.TabIndex = 1;
			this.BarcodeInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckForEnter);
			// 
			// ProgramImageBox
			// 
			this.ProgramImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgramImageBox.Location = new System.Drawing.Point(219, 27);
			this.ProgramImageBox.Name = "ProgramImageBox";
			this.ProgramImageBox.Size = new System.Drawing.Size(188, 355);
			this.ProgramImageBox.TabIndex = 2;
			this.ProgramImageBox.TabStop = false;
			// 
			// ClearBtn
			// 
			this.ClearBtn.Location = new System.Drawing.Point(12, 329);
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(95, 53);
			this.ClearBtn.TabIndex = 3;
			this.ClearBtn.Text = "Clear";
			this.ClearBtn.UseVisualStyleBackColor = true;
			this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
			// 
			// LoadSingleBtn
			// 
			this.LoadSingleBtn.Location = new System.Drawing.Point(117, 329);
			this.LoadSingleBtn.Name = "LoadSingleBtn";
			this.LoadSingleBtn.Size = new System.Drawing.Size(95, 53);
			this.LoadSingleBtn.TabIndex = 4;
			this.LoadSingleBtn.Text = "Load Single";
			this.LoadSingleBtn.UseVisualStyleBackColor = true;
			this.LoadSingleBtn.Click += new System.EventHandler(this.LoadSingleBtn_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(419, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 394);
			this.Controls.Add(this.LoadSingleBtn);
			this.Controls.Add(this.ClearBtn);
			this.Controls.Add(this.ProgramImageBox);
			this.Controls.Add(this.BarcodeInput);
			this.Controls.Add(this.SingleProgramList);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.ProgramImageBox)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox SingleProgramList;
		private System.Windows.Forms.TextBox BarcodeInput;
		private System.Windows.Forms.PictureBox ProgramImageBox;
		private System.Windows.Forms.Button ClearBtn;
		private System.Windows.Forms.Button LoadSingleBtn;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
	}
}

