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
		private WallpaperCreator Creator = new WallpaperCreator();
		private WallpaperConfigManager Loader_Saver = new WallpaperConfigManager();
		private WallpaperConfigCollection Configurations = new WallpaperConfigCollection();
		private EventHandler DisplaySettingsChangedEventHandler;
		private EventHandler ChangeWallpaperEventHandler;

		public SimpleTestForm() {
			InitializeComponent();
			LoadConfiguration();
			IntializeEventHandlers();
			_NotifyIcon.Icon = Icon;
			WallpaperPicker_ConfigChanged(null, new ConfigChangedEventArgs(Configurations[0]));
		}

		private void LoadConfiguration() {
			WallpaperChangerConfig config = Loader_Saver.Load();
			if (config == null) {
				config = WallpaperChangerConfig.GetDefault(Screen.AllScreens.Length);
			}
			Configurations = config.Screens;
			InitScreens();
			//-- Load Configuration for the Primary Monitor
			_WallpaperPicker.Config = Configurations[0];
		}

		private void InitScreens() {
			Creator.Dispose();
			Creator = new WallpaperCreator();
			for (int i = 1; i < Configurations.Count; i++) {
				if (Screen.AllScreens.Length > i)
					Creator.InitScreen(i, Configurations[i]);
			}
		}

		private void IntializeEventHandlers() {
			_WallpaperPicker.ConfigChanged +=
				new EventHandler<ConfigChangedEventArgs>(WallpaperPicker_ConfigChanged);
			DisplaySettingsChangedEventHandler = new EventHandler(DisplaySettingsChanged);
			ChangeWallpaperEventHandler = new EventHandler(ChangeWallpaper);
			SystemEvents.DisplaySettingsChanged += DisplaySettingsChangedEventHandler;
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
		}

		/// <summary>
		/// Changes one or all wallpapers, depending on
		/// the tag of the ToolStripMenu item sender
		/// </summary>
		void ChangeWallpaper(object sender, EventArgs e) {
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			int index = (int)tsmi.Tag;
			if (index == -1)
				QuickChanger.ChangeAllWallpapers();
			else
				QuickChanger.ChangeWallpaper(index);
		}

		/// <summary>
		/// We need to detach the DisplaySettingsChangedEventHandler because it's
		/// attached to a static event. Withought this, our application can
		/// generate memory leaks.
		/// </summary>
		void Application_ApplicationExit(object sender, EventArgs e) {
			SystemEvents.DisplaySettingsChanged -= DisplaySettingsChangedEventHandler;
		}

		void DisplaySettingsChanged(object sender, EventArgs e) {
			InitScreens();
			_PreviewImageBox.Image = Creator.PreviewBitmap;
			SaveAndSetWallpaper();
		}

		void WallpaperPicker_ConfigChanged(object sender, ConfigChangedEventArgs e) {
			int idx = Creator.SelectedIndex;
			if (idx != -1) {
				Creator.InitScreen(idx, e.Config);
				//-- Reset Preview Image
				_PreviewImageBox.Image = Creator.PreviewBitmap;
			}
		}

		private void PreviewBox_MouseClick(object sender, MouseEventArgs e) {
			int index = Creator.GetIndexFromPointOnPreviewImage(_PreviewImageBox.MousePositionOnImage);
			if (index != -1) {
				//-- Save Current config to Configurations
				Configurations[Creator.SelectedIndex] = _WallpaperPicker.Config;
				//-- Set New Selected Index for Creator
				Creator.SelectedIndex = index;
				//-- Initialize WallpaperConfig to be the new configuration
				_WallpaperPicker.Config = Configurations[index];
				//-- Get the preview image to display the border around the newly selected image
				_PreviewImageBox.Image = Creator.PreviewBitmap;
			}
		}

		private void StretchWallpaperAllScreensCB_CheckedChanged(object sender, EventArgs e) {
			if (_StretchWallpaperAllScreensCB.Checked) {
				MessageBox.Show("Wallpaper is now stretched across all screens -- NEED TO IMPLEMENT");
			}
		}

		private void SimpleTestForm_Resize(object sender, EventArgs e) {
			_MainSplitContainer.SplitterDistance = _MainSplitContainer.Height - _MainSplitContainer.Panel2MinSize;
		}


		#region Helper Methods
		private void SaveAndSetWallpaper() {
			Loader_Saver.Save(new WallpaperChangerConfig(Configurations));
			string path = Loader_Saver.GetWallpaperPath();
			Creator.DesktopBitmap.Save(path, ImageFormat.Bmp);
			WallpaperManager.SetWallpaper(path);
		}

		#endregion

		#region Button Events
		private void ApplyButton_Click(object sender, EventArgs e) {
			SaveAndSetWallpaper();
		}

		private void CancelButton_Click(object sender, EventArgs e) {
			LoadConfiguration();
			Hide();
		}

		private void OKButton_Click(object sender, EventArgs e) {
			SaveAndSetWallpaper();
			Hide();
		}

		private void OpenDisplayPropertiesButton_Click(object sender, EventArgs e) {
			Process.Start(DisplayProperties);
		}

		#endregion	

		private void ShowWallMaster(object sender, EventArgs e) {
			Show();
			Activate();
		}

		private void SimpleTestForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (e.CloseReason != CloseReason.ApplicationExitCall &&
				e.CloseReason != CloseReason.WindowsShutDown) {
				Hide();
				e.Cancel = true;
			}
		}

		private void _NotifyIcon_DoubleClick(object sender, EventArgs e) {
			Show();
			Activate();
		}

		private void Exit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		/// <summary>
		/// This method is necessary to dynamically populate
		/// the change screen menu items. It'll allow the use to change
		/// 1 or all of the screens if their are multiple screens, or just
		/// one screen if there is just one.
		/// </summary>
		private void contextMenuStrip1_Opened(object sender, CancelEventArgs e) {
			_CMI_ChangeWallpaper.DropDownItems.Clear();

			//-- If we only have 1 monitor, we don't need to have any drop downs
			if (Screen.AllScreens.Length == 1) {
				_CMI_ChangeWallpaper.Click -= ChangeWallpaperEventHandler;
				_CMI_ChangeWallpaper.Click += ChangeWallpaperEventHandler;
				_CMI_ChangeWallpaper.Tag = 0;
			} else { //-- More then 1 monitor
				_CMI_ChangeWallpaper.Click -= ChangeWallpaperEventHandler;
				_CW_ChangeAllWallpapers.Tag = -1;
				_CMI_ChangeWallpaper.DropDownItems.Add(_CW_ChangeAllWallpapers);
				_CMI_ChangeWallpaper.DropDownItems.Add(new ToolStripSeparator());
				for (int i = 0; i < Screen.AllScreens.Length; i++) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(string.Format("Change Screen {0}", (i + 1)));
					tsmi.Click += ChangeWallpaperEventHandler;
					tsmi.Tag = i;
					_CMI_ChangeWallpaper.DropDownItems.Add(tsmi);
				}
			}
		}
	}
}
