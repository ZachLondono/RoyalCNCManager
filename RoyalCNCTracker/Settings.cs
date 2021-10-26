using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoyalCNCTracker {
	public partial class Settings : Form {

		private Configuration config { get; set; }

		public Settings() {
			InitializeComponent();

			config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var settings = config.AppSettings.Settings;

			var output = settings["Output"];
			if (output is not null) OutputPathText.Text = output.Value;

			var source = settings["Source"];
			if (source is not null) DataSourceText.Text = source.Value;

			var data = settings["Data"];
			if (data is not null) TrackingText.Text = data.Value;

			var images = settings["ImageSource"];
			if (images is not null) ImageText.Text = images.Value;


			var singles = settings["SinglesSource"];
			if (singles is not null) SinglesText.Text = singles.Value;

		}

		private void EditOutputButton_Click(object sender, EventArgs e) {
			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
				OutputPathText.Text = folderBrowserDialog.SelectedPath;
		}

		private void EditSourceBtn_Click(object sender, EventArgs e) {
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
				DataSourceText.Text = openFileDialog1.FileName;
		}

		private void EditTrackingBtn_Click(object sender, EventArgs e) {
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
				TrackingText.Text = openFileDialog1.FileName;
		}

		private void SaveBtn_Click(object sender, EventArgs e) {
			
			var settings = config.AppSettings.Settings;
			settings.Remove("Output");
			settings.Add("Output", OutputPathText.Text);

			settings.Remove("Source");
			settings.Add("Source", DataSourceText.Text);

			settings.Remove("Data");
			settings.Add("Data", TrackingText.Text);

			settings.Remove("ImageSource");
			settings.Add("ImageSource", ImageText.Text);

			settings.Remove("SinglesSource");
			settings.Add("SinglesSource", SinglesText.Text);

			config.Save();
		}

		private void EditImageBtn_Click(object sender, EventArgs e) {
			var result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
				ImageText.Text = folderBrowserDialog.SelectedPath;
		}

		private void EditSinglesBtn_Click(object sender, EventArgs e) {
			var result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
				SinglesText.Text = folderBrowserDialog.SelectedPath;
		}
	}
}
