
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
			((System.ComponentModel.ISupportInitialize)(this.ProgramImageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// SingleProgramList
			// 
			this.SingleProgramList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.SingleProgramList.FormattingEnabled = true;
			this.SingleProgramList.ItemHeight = 15;
			this.SingleProgramList.Location = new System.Drawing.Point(13, 42);
			this.SingleProgramList.Name = "SingleProgramList";
			this.SingleProgramList.Size = new System.Drawing.Size(200, 274);
			this.SingleProgramList.TabIndex = 0;
			// 
			// BarcodeInput
			// 
			this.BarcodeInput.Location = new System.Drawing.Point(13, 13);
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
			this.ProgramImageBox.Location = new System.Drawing.Point(219, 12);
			this.ProgramImageBox.Name = "ProgramImageBox";
			this.ProgramImageBox.Size = new System.Drawing.Size(188, 370);
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
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.ProgramImageBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox SingleProgramList;
		private System.Windows.Forms.TextBox BarcodeInput;
		private System.Windows.Forms.PictureBox ProgramImageBox;
		private System.Windows.Forms.Button ClearBtn;
		private System.Windows.Forms.Button LoadSingleBtn;
	}
}

