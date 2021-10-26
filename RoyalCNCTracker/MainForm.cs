using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RoyalCNCTrackerLib;
using RoyalCNCTrackerLib.DAL;
using RoyalCNCTrackerLib.DAL.Sqlite;
using RoyalCNCTrackerLib.DAL.MSAccess;
using RoyalCNCTrackerLib.Models;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.IO;

namespace RoyalCNCTracker {
	public partial class MainForm : Form {

		private readonly Processor processor;
		private readonly SqliteConnection _connection;
		private Image _originalCropped { get; set; }

		public MainForm() {
			InitializeComponent();

			string output = ConfigurationManager.AppSettings.Get("Output");
			if (string.IsNullOrEmpty(output)) output = "C:\\Users\\Zachary Londono\\Desktop\\PATTERNS";
			string source = ConfigurationManager.AppSettings.Get("Source");
			if (string.IsNullOrEmpty(source)) source = "C:\\Users\\Zachary Londono\\Documents\\GitHub\\RoyalCNC\\CNCLibTests\\TestData\\CLindex.mdb";
			string tracker = ConfigurationManager.AppSettings.Get("Data");
			if (string.IsNullOrEmpty(tracker)) tracker = "Y:\\CADCode\\CNC Barcode Reader\\tracker.db";
			string image = ConfigurationManager.AppSettings.Get("ImageSource");
			if (string.IsNullOrEmpty(image)) image = "Y:\\CADCode\\pix"; 
			string singles = ConfigurationManager.AppSettings.Get("SinglesSource");
			if (string.IsNullOrEmpty(singles)) singles = "Y:\\CADCode\\CNC Programs\\Omnitech";

			_connection = new SqliteConnection($"Data Source='{tracker}';");

			ICCRepository barcodeDb = new AccessCCRepository(source);
			ILabelDBFactory labelDbFactory = new AccessLabelDBFactory(barcodeDb, image, singles);
			IJobRepository trackerDb = new SqliteJobRepository(_connection);
			IProgramRepository programDb = new SqliteProgramRepository(_connection);

			processor = new Processor(output, image, barcodeDb, trackerDb, programDb, labelDbFactory);

		}

		// <summary>
		// Checks if the last key entered was an enter character, and if it is get the current input in the text box to set the current program
		// </summary>
		private void CheckForEnter(object sender, KeyEventArgs e) {

			if (e.KeyCode != Keys.Enter) return;

			string input = BarcodeInput.Text;

			_connection.Open();
			CADCodeProgram program = processor.GetCADCodeProgram(input);
			_connection.Close();

			SingleProgramList.Items.Clear();
			SetCurrentProgram(program);

		}
		
		// <summary>
		// Clears the single programs listed in the listbox, the image in the image box, and the files in the output path
		// </summary>
		private void ClearBtn_Click(object sender, EventArgs e) {

			BarcodeInput.Clear();
			SingleProgramList.Items.Clear();
			ProgramImageBox.Visible = false;

			try {

				string output = ConfigurationManager.AppSettings.Get("Output");
				if (string.IsNullOrEmpty(output))
					throw new InvalidOperationException("Output directory is not set");

				var files = Directory.EnumerateFiles(output, "*", SearchOption.TopDirectoryOnly);

				if (files.Count() >= 10) {
					var result = MessageBox.Show($"You are about to delete '{files.Count()}' files from\n'{output}'\n\nContinue?",
												"File Delete Warning",
												MessageBoxButtons.YesNo,
												MessageBoxIcon.Warning);

					if (result == DialogResult.No) return;	
				}

				foreach(var file in files) {
					Debug.WriteLine($"Deleting file: '{file}'");
					File.Delete(file);
				}


			} catch (Exception ex) {
				var result = MessageBox.Show($"Error clearing directory\nShow error details?\n\n[{ex.Message}]",
											"Unable to clear ouput directory",
											MessageBoxButtons.YesNo,
											MessageBoxIcon.Error);
				if (result == DialogResult.Yes) {
					MessageBox.Show(ex.ToString(), "Error Message");
				}
			}


		}

		// <summary>
		// Loads the selected single program from the list box 
		// <summary>
		private void LoadSingleBtn_Click(object sender, EventArgs e) {

			CADCodeProgram selected = SingleProgramList.SelectedItem as CADCodeProgram;

			if (selected is null) return;

			SetCurrentProgram(selected);

		}

		private void SetCurrentProgram(CADCodeProgram program) {

			BarcodeInput.Clear();
			ProgramImageBox.Visible = false;

			string imagepath = program.ImageFile;

			bool crop = true;
			if (program is PatternProgram) {

				PatternProgram pattern = program as PatternProgram;

				foreach (CADCodeProgram component in pattern.GetComponents()) {
					SingleProgramList.Items.Add(component);
				}
				crop = true;
			} else {
				processor.CopyProgramToLocal(program);
				crop = false;
			}


			try {
				SetPatternPicture(imagepath, crop);
			} catch (Exception ex) {
				Debug.WriteLine("Error setting image\n" + ex.ToString());
			}

		}

		private void SetPatternPicture(string path, bool crop) {

			Bitmap image = new Bitmap(path);

			if (crop) {
				// Scale the image based on the windows display scale settings
				double graphicsScale = 96.0f / CreateGraphics().DpiX;

				// Crop the image to get reid of all of the transparent space around the actual image
				Rectangle cropRect = new Rectangle();
				cropRect.Width = (int)(530.0f * 1 / graphicsScale);
				cropRect.Height = (int)(1035.0f * 1 / graphicsScale);

				var cropped = image.Clone(cropRect, image.PixelFormat);

				// Save the original image (after cropping) so that it can be scaled when window size changes
				image = cropped;
			}

			_originalCropped = image;
				
			//PatternImage.SizeMode = PictureBoxSizeMode.Normal;
			ProgramImageBox.Image = ScaleImageToPictureBox(image);

			ProgramImageBox.Visible = true;

		}

		private Image ScaleImageToPictureBox(Image image) {

			double widthScale;
			if (image.Width >= ProgramImageBox.Width)
				widthScale = ((double)ProgramImageBox.Width) / image.Width;
			else widthScale = 1;

			double heightScale;
			if (image.Height >= ProgramImageBox.Height)
				heightScale = ((double)ProgramImageBox.Height) / image.Height;
			else heightScale = 1;

			double scale;
			if (widthScale > heightScale) scale = heightScale;
			else scale = widthScale;

			return new Bitmap(image, new Size((int)((double)image.Width * scale), (int)((double)image.Height * scale)));

		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {

			Settings settings = new Settings();
			settings.Visible = true;

		}
	}
}
