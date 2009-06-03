using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WallpaperUtils;
using System.Diagnostics;
using Microsoft.Win32;

namespace WallpaperChanger {
	public partial class SimpleTestForm : Form {

		protected ProcessStartInfo DisplayProperties {
			get { 
				return
				new ProcessStartInfo()
				{
					FileName = "control.exe",
					Arguments = "desk.cpl,,3"
				};
			}
		}
		WallpaperCreator creator = new WallpaperCreator();
		WallpaperManager manager = new WallpaperManager();
		WallpaperConfigManager loader_saver = new WallpaperConfigManager();
		WallpaperConfigCollection configurations = new WallpaperConfigCollection();

		public SimpleTestForm() {
			InitializeComponent();
			LoadConfiguration();
			IntializeEventHandlers();
			WallpaperPicker_ConfigChanged(null, new ConfigChangedEventArgs(configurations[0]));
		}

		private void LoadConfiguration() {
			WallpaperChangerConfig config = loader_saver.Load();
			configurations = config.Screens;
			for (int i = 1; i < configurations.Count; i++) {
				if (Screen.AllScreens.Length > i)
					creator.InitScreen(i, configurations[i]);
			}
			//-- Load Configuration for the Primary Monitor
			_WallpaperPicker.Config = configurations[0];
		}

		private void IntializeEventHandlers() {
			_WallpaperPicker.ConfigChanged +=
				new EventHandler<ConfigChangedEventArgs>(WallpaperPicker_ConfigChanged);
			SystemEvents.DisplaySettingsChanged += new EventHandler(DisplaySettingsChanged);
		}

		void DisplaySettingsChanged(object sender, EventArgs e) {
			_PreviewImageBox.Image = creator.PreviewBitmap;
			SetWallpaper();
		}

		void WallpaperPicker_ConfigChanged(object sender, ConfigChangedEventArgs e) {
			int idx = creator.SelectedIndex;
			if (idx != -1) {
				creator.InitScreen(idx, e.Config);
				//-- Reset Preview Image
				_PreviewImageBox.Image = creator.PreviewBitmap;
			}
		}

		private void PreviewBox_MouseClick(object sender, MouseEventArgs e) {
			int index = creator.GetIndexFromPointOnPreviewImage(_PreviewImageBox.MousePositionOnImage);
			if (index != -1) {
				configurations[creator.SelectedIndex] = _WallpaperPicker.Config;
				creator.SelectedIndex = index;
				_WallpaperPicker.Config = configurations[index];
			}
		}

		private void OpenDisplayPropertiesButton_Click(object sender, EventArgs e) {
			Process.Start(DisplayProperties);
		}

		private void StretchWallpaperAllScreensCB_CheckedChanged(object sender, EventArgs e) {
			if (_StretchWallpaperAllScreensCB.Checked) {
				MessageBox.Show("Wallpaper is now stretched across all screens -- NEED TO IMPLEMENT");
			}
		}

		private void SimpleTestForm_Resize(object sender, EventArgs e) {
			_MainSplitContainer.SplitterDistance = _MainSplitContainer.Height - _MainSplitContainer.Panel2MinSize;
		}

		private void ApplyButton_Click(object sender, EventArgs e) {
			loader_saver.Save(new WallpaperChangerConfig(configurations));
			//try {
				SetWallpaper();
			//} catch (Exception exp) {
			//  MessageBox.Show(exp.Message, "Exception");
			//}
		}

		#region Helper Methods
		private void SetWallpaper() {
			string path = loader_saver.GetWallpaperPath();
			creator.DesktopBitmap.Save(path, ImageFormat.Bmp);
			WallpaperManager.SetWallpaper(path);
		}
		#endregion

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e) {

		}
	}
}
