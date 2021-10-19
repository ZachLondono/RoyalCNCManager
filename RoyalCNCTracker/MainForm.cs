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

namespace RoyalCNCTracker {
	public partial class MainForm : Form {

		private readonly Processor processor;
		private Image _originalCropped { get; set; }

		public MainForm() {
			InitializeComponent();

			string patternPath = "C:\\Users\\Zachary Londono\\Desktop\\PATTERNS";
			string imagePath = "Y:\\CADCode\\pix";
			string barcodePath = "C:\\Users\\Zachary Londono\\Documents\\GitHub\\RoyalCNC\\CNCLibTests\\TestData\\CLindex.mdb";
			string jobPath = "C:\\Users\\Zachary Londono\\Documents\\GitHub\\RoyalCNC\\CNCLibTests\\TestData\\tracker.db"; ;

			ICCRepository barcodeDb = new AccessCCRepository(barcodePath);
			IJobRepository trackerDb = new SqliteJobRepository(jobPath);
			IProgramRepository programDb = new SqliteProgramRepository(jobPath);
			ILabelDBFactory labelDbFactory = new AccessLabelDBFactory(barcodeDb, imagePath, patternPath);

			processor = new Processor(patternPath, imagePath, barcodeDb, trackerDb, programDb, labelDbFactory);

		}

		// <summary>
		// Checks if the last key entered was an enter character, and if it is get the current input in the text box to set the current program
		// </summary>
		private void CheckForEnter(object sender, KeyEventArgs e) {

			if (e.KeyCode != Keys.Enter) return;

			string input = BarcodeInput.Text;
			SetCurrentProgram(input);

		}
		
		// <summary>
		// Clears the single programs listed in the listbox, the image in the image box, and the files in the output path
		// </summary>
		private void ClearBtn_Click(object sender, EventArgs e) {

			BarcodeInput.Clear();
			SingleProgramList.Items.Clear();
			ProgramImageBox.Visible = false;

			// TODO: clear output directory

		}

		// <summary>
		// Loads the selected single program from the list box 
		// <summary>
		private void LoadSingleBtn_Click(object sender, EventArgs e) {

			CADCodeProgram selected = SingleProgramList.SelectedItem as CADCodeProgram;

			if (selected is null) return;

			SetCurrentProgram(selected.ProgramName);

		}

		private void SetCurrentProgram(string programName) {

			BarcodeInput.Clear();
			SingleProgramList.Items.Clear();
			ProgramImageBox.Visible = false;

			CADCodeProgram program = processor.GetCADCodeProgram(programName);
			string imagepath = program.ImageFile;

			try {
				SetPatternPicture(imagepath);
			} catch (Exception ex) {
				Debug.WriteLine("Error setting image\n" + ex.ToString());
			}

			if (program is PatternProgram) {

				PatternProgram pattern = program as PatternProgram;

				foreach (CADCodeProgram component in pattern.GetComponents()) {
					SingleProgramList.Items.Add(component);
				}

			}

		}

		private void SetPatternPicture(string path) {

			Bitmap original = new Bitmap(path);

			// Scale the image based on the windows display scale settings
			double graphicsScale = 96.0f / CreateGraphics().DpiX;

			// Crop the image to get reid of all of the transparent space around the actual image
			Rectangle cropRect = new Rectangle();
			cropRect.Width = (int)(530.0f * 1 / graphicsScale);
			cropRect.Height = (int)(1035.0f * 1 / graphicsScale);

			var cropped = original.Clone(cropRect, original.PixelFormat);

			// Save the original image (after cropping) so that it can be scaled when window size changes
			_originalCropped = cropped;
			original.Dispose();

			//PatternImage.SizeMode = PictureBoxSizeMode.Normal;
			ProgramImageBox.Image = ScaleImageToPictureBox(cropped);

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

	}
}
