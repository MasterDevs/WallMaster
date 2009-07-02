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
			CurrentIndex = 0;
			//-- Load Primary Monitor
			WallpaperPicker_ConfigChanged(null, new ConfigChangedEventArgs(Configurations[CurrentIndex]));
			UserHasMadeAChange = false;
		}

		#region Initialization

		private void LoadConfiguration() {
			_WallpaperPicker.RaiseEvents = false;
			WallpaperChangerConfig config = Loader_Saver.Load();
			if (config == null) {
				config = WallpaperChangerConfig.GetDefault(Screen.AllScreens.Length);
			}
			Configurations = config.Screens;
			InitScreens();
			_WallpaperPicker.Config = Configurations[CurrentIndex];
			_WallpaperPicker.RaiseEvents = true;
		}

		private void InitScreens() {
			Creator.Dispose();
			Creator = new WallpaperCreator();
			for (int i = 0; i < Configurations.Count; i++) {
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

		#endregion

		#region Events

		/// <summary>
		/// We need to detach the DisplaySettingsChangedEventHandler because it's
		/// attached to a static event. Withought this, our application can
		/// generate memory leaks.
		/// </summary>
		private void Application_ApplicationExit(object sender, EventArgs e) {
			SystemEvents.DisplaySettingsChanged -= DisplaySettingsChangedEventHandler;
		}

		private void DisplaySettingsChanged(object sender, EventArgs e) {
			InitScreens();
			_PreviewImageBox.Image = Creator.PreviewBitmap;
			SaveAndSetWallpaper();
		}

		private void WallpaperPicker_ConfigChanged(object sender, ConfigChangedEventArgs e) {
			int idx = Creator.SelectedIndex;
			if (idx != -1) {
				Creator.InitScreen(idx, e.Config);
				//-- Reset Preview Image
				_PreviewImageBox.Image = Creator.PreviewBitmap;
			}
			//-- If we are changing configurations because we are selecting a different
			// wallpaper, then the user has not made a change.
			if(CurrentIndex == idx)
				UserHasMadeAChange = true;

			CurrentIndex = idx;
		}

		private void PreviewBox_MouseClick(object sender, MouseEventArgs e) {
			int index = Creator.GetIndexFromPointOnPreviewImage(_PreviewImageBox.MousePositionOnImage);
			if (index != -1) {
				//-- Save Current config to Configurations
				Configurations[Creator.SelectedIndex] = _WallpaperPicker.Config;
				//-- Set New Selected Index for Creator
				Creator.SelectedIndex = index;
				//-- Pause Raising Config Changed Event
				_WallpaperPicker.RaiseEvents = false;
				//-- Initialize WallpaperConfig to be the new configuration
				_WallpaperPicker.Config = Configurations[index];
				//-- Get the preview image to display the border around the newly selected image
				_PreviewImageBox.Image = Creator.PreviewBitmap;
				//-- Resume Raising Config Changed Event
				_WallpaperPicker.RaiseEvents = true;
				//-- Change Current Index
				CurrentIndex = index;
			}
		}

		#region Form Events
		private void SimpleTestForm_Resize(object sender, EventArgs e) {
			_MainSplitContainer.SplitterDistance = _MainSplitContainer.Height - _MainSplitContainer.Panel2MinSize;
		}

		private void SimpleTestForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (e.CloseReason != CloseReason.ApplicationExitCall &&
				e.CloseReason != CloseReason.WindowsShutDown) {
				Hide();
				e.Cancel = true;
			}
		} 
		#endregion

		#region Button Events
		private void ApplyButton_Click(object sender, EventArgs e) {
			SaveAndSetWallpaper();
			UserHasMadeAChange = false;
		}

		private void CancelButton_Click(object sender, EventArgs e) {
			LoadConfiguration();
			UserHasMadeAChange = false;
			Hide();
		}

		private void OKButton_Click(object sender, EventArgs e) {
			if (UserHasMadeAChange) {
				SaveAndSetWallpaper();
				UserHasMadeAChange = false;
			}
			Hide();
		}

		private void OpenDisplayProperties_Click(object sender, EventArgs e) {
			Process.Start(DisplayProperties);
		}

		#endregion	

		#region Notify Icon & Context Menu Events

		private void ShowWallMaster(object sender, EventArgs e) {
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
		private void ChangeWallpaperMenuStrip_Opened(object sender, CancelEventArgs e) {
			PopulateMenuItems(_CMI_ChangeWallpaper, _CMI_CW_ChangeAllWallpapers);
		}

		private void _MMS_File_DropDownOpening(object sender, EventArgs e) {
			PopulateMenuItems(_FMI_ChangeWallpaper, _FMI_CW_ChangeAllWallpapers);
		}

		private void PopulateMenuItems(ToolStripMenuItem changeWallpaper, ToolStripMenuItem changeAll) {
			changeWallpaper.DropDownItems.Clear();
			changeWallpaper.Click -= ChangeWallpaperEventHandler;
			//-- If we only have 1 monitor, we don't need to have any drop downs
			if (Screen.AllScreens.Length == 1) {
				changeWallpaper.Click += ChangeWallpaperEventHandler;
				changeWallpaper.Tag = 0;
			} else { //-- More then 1 monitor
				changeAll.Tag = -1;
				changeWallpaper.DropDownItems.Add(changeAll);

				changeWallpaper.DropDownItems.Add(new ToolStripSeparator());
				for (int i = 0; i < Screen.AllScreens.Length; i++) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(string.Format("Change Screen {0}", (i + 1)));
					tsmi.Click += ChangeWallpaperEventHandler;
					tsmi.Tag = i;
					changeWallpaper.DropDownItems.Add(tsmi);
				}
			}
		}

		/// <summary>
		/// Changes one or all wallpapers, depending on
		/// the tag of the ToolStripMenu item sender
		/// </summary>
		private void ChangeWallpaper(object sender, EventArgs e) {
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			int index = (int)tsmi.Tag;
			if (index == -1)
				QuickChanger.ChangeAllWallpapers();
			else
				QuickChanger.ChangeWallpaper(index);

			//-- Pause raising all events so we don't have to load the picture box multiple times
			LoadConfiguration();

			//-- Make sure we highlight the same box
			Creator.SelectedIndex = CurrentIndex;

			//-- Manually refresh the picture box
			_PreviewImageBox.Image = Creator.PreviewBitmap;
		}

		#endregion
		
		#endregion

		#region Helper Methods
		private void SaveAndSetWallpaper() {
			Loader_Saver.Save(new WallpaperChangerConfig(Configurations));
			string path = Loader_Saver.GetWallpaperPath();
			Creator.DesktopBitmap.Save(path, ImageFormat.Bmp);
			WallpaperManager.SetWallpaper(path);
		}
		
		/// <summary>
		/// This property will be used to denote wheter or not a user has made a change.
		/// </summary>
		private bool UserHasMadeAChange { 
			get { return _ApplyButton.Enabled; }
			set { _ApplyButton.Enabled = value; }
		}

		private int CurrentIndex { get; set; }
		#endregion
	}
}
