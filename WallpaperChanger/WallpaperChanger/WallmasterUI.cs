using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WallpaperUtils;
using Microsoft.Win32;

namespace WallpaperChanger {
	public partial class WallmasterUI : Form {

		private WallpaperConfigManager _wcm;
		private WallpaperChangerConfig _cfg;
		private WallpaperCreator _creator = new WallpaperCreator();

		#region CTOR
		public WallmasterUI() {
			InitializeComponent();

			_wcm = new WallpaperConfigManager();
			LoadConfig();

			setImageBox();
			SystemEvents.DisplaySettingsChanged += new EventHandler(_sm_ScreenChanged);
			setTimer();
		}
		#endregion

		#region Menus
		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Exit();
		}

		private void changeWallpaperToolStripMenuItem_Click(object sender, EventArgs e) {
			changeAll();
			setWallpaper();
		}

		private void showHideApplicationToolStripMenuItem_Click(object sender, EventArgs e) {
			toggleVIsibility();
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
			Exit();
		}

		#endregion


		private void setImageBox() {
			Image i = GenerateWallpaper();
			_picBox.Image = i;
		}

		#region Form Events
		private void _setWPButton_Click(object sender, EventArgs e) {
			setWallpaper();
		}


		private void multiWpPicker_ConfigChanged(object sender, ConfigsChangedEventArgs e) {
			try {
				_changeAllButton.Enabled = e.Configs.ContainsRandom;
				setImageBox();

			} catch (Exception ex) {
				displayError(ex);
			}
		}


		/// <summary>
		/// Toggle form visibility when notification (tray) icon is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			changeAll();
			setWallpaper();

		}

		/// <summary>
		/// Hide form when minimized
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_SizeChanged(object sender, EventArgs e) {
			if (this.WindowState == FormWindowState.Minimized) {
				this.Visible = false;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveConfig();
		}


		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			LoadConfig();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
			// Remove the icon from the notification area before closing
			_notifyIcon.Visible = false;

			SaveConfig();
		}



		#endregion

		#region Helpers
		private void displayError(Exception ex) {
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}



		private Image GenerateWallpaper() {
			Size[] s = Screen.AllScreens.ToSizes();
			WallpaperConfigCollection wcc = multiWpPicker.Config;
			return GenerateWallpaper(s, wcc);
		}

		private static Image GenerateWallpaper(Size[] s, WallpaperConfigCollection wcc) {
			PaneledImageBuilder ib = new PaneledImageBuilder();
			Color[] c = wcc.GetColors();
			Image[] i = wcc.GetImages();
			WallpaperStretchStyle[] ss = wcc.GetStretchStyles();

			ib.SetSizes(s);
			ib.SetColors(c);
			ib.SetImages(i);
			ib.SetStretchStyles(ss);

			Image genI = ib.BuildImage();
			return genI;
		}

		private Image[] getImages() {
			throw new NotImplementedException();
		}

		private Color[] getColors() {
			throw new NotImplementedException();
		}

		private Size[] getSizes() {
			throw new NotImplementedException();
		}

		private static void s_BrowsePath(OpenFileDialog fd, TextBox pathTB) {
			DialogResult dr = fd.ShowDialog();
			if (dr == DialogResult.OK) {
				s_SetPath(fd.FileName, pathTB);
			}
		}

		private static void s_SetPath(string p, TextBox tb) {
			tb.Text = p;
		}


		#endregion



		void _sm_ScreenChanged(object sender, EventArgs e) {
			setWallpaper();
			setImageBox();
		}


		private void SaveConfig() {
			//WallpaperConfigCollection cfg = multiWpPicker.Config;
			_cfg.Screens = multiWpPicker.Config;
			_cfg.CycleWallpaperRate = timeSpanPicker1.TimeSpan;

			_wcm.Save(_cfg);
		}


		private void LoadConfig() {
			_cfg = _wcm.Load();
			if (_cfg == null) {
				_cfg = _wcm.CreateEmptyConfig(Screen.AllScreens.Length);
			}

			// If we have more screens than the last time, add enough blanks screens
			if (Screen.AllScreens.Length > _cfg.Screens.Count) {
				int count = Screen.AllScreens.Length - _cfg.Screens.Count;
				int offset = _cfg.Screens.Count;
				_cfg.Screens.AddScreen(offset, count);
			}

			multiWpPicker.Config = _cfg.Screens;
			timeSpanPicker1.TimeSpan = _cfg.CycleWallpaperRate;
			
		}

		private void Exit() {
			_notifyIcon.Visible = false;
			Application.Exit();
		}

		private void _changeAllButton_Click(object sender, EventArgs e) {
			changeAll();
			setWallpaper();
		}

		private void changeAll() {
			multiWpPicker.ChangeRandomImage();
		}


		/// <summary>
		/// Show/hide the main form
		/// </summary>
		private void toggleVIsibility() {
			this.Visible = !this.Visible;

			if (this.Visible) {
				this.WindowState = FormWindowState.Normal;
				this.BringToFront();
			}
		}

		private void setWallpaper() {
			try {
				string path = _wcm.GetWallpaperPath();
				using (Image i = GenerateWallpaper()) {
					i.Save(path, ImageFormat.Bmp);
				}
				
				WallpaperManager.SetWallpaper(path);

				SaveConfig();
			} catch (Exception ex) {
				displayError(ex);
			}
		}

		private void _changePaperTimer_Tick(object sender, EventArgs e) {
			changeAll();
			setWallpaper();
		}

		private void timeSpanPicker1_TimeSpanPickerValueChanged(object sender, TimeSpanPickerValueChangedEventArgs e) {
			setTimer(e.TimeSpan);
		}

		private void setTimer() {
			setTimer(timeSpanPicker1.TimeSpan);
		}


		private void setTimer(TimeSpan ts) {
			if (ts.TotalMilliseconds > 0) {
				_changePaperTimer.Interval = (int)ts.TotalMilliseconds;
				_changePaperTimer.Enabled = true;
			}
			else {
				_changePaperTimer.Enabled = false;
			}
		}


	}
}