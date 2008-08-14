using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger {
	public partial class TestForm : Form {
		private const string PATH = @"c:\wallpaper.bmp";

		#region Private fields
		private Color _color1;
		private Color _color2;

		#endregion

		#region CTOR
		public TestForm() {
			_color1 = Color.Black;
			_color2 = Color.Black;
			InitializeComponent();

			ScreenMonitor.ScreenMonitor sm = new ScreenMonitor.ScreenMonitor(100);
			sm.ScreenChanged += new EventHandler(sm_ScreenChanged);
			sm.Start();

			LoadConfig();
		}

		void sm_ScreenChanged(object sender, EventArgs e) {
			MessageBox.Show("Screen Changed");
		}
		#endregion

		#region Menus
		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void changeWallpaperToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void showHideApplicationToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		#endregion


		#region Form Events
		private void _setWPButton_Click(object sender, EventArgs e) {
			try {
				using (Image i = GenerateWallpaper()) {
					i.Save(PATH, ImageFormat.Bmp);
				}

				WallpaperManager.SetWallpaper(PATH);
			} catch (Exception ex) {
				displayError(ex);
			}
		}


		private void _browseButton_Click(object sender, EventArgs e) {
			s_BrowsePath(_openFileDialog, _wpPath1);
		}

		private void _browseButton2_Click(object sender, EventArgs e) {
			s_BrowsePath(_openFileDialog, _wpPath2);
		}

		private void _togglePathsButton_Click(object sender, EventArgs e) {
			string swap = _wpPath1.Text;
			_wpPath1.Text = _wpPath2.Text;
			_wpPath2.Text = swap;

		}

		private void _genImageButton_Click(object sender, EventArgs e) {
			try {
				Size[] s = getSizes();

				Image genI = GenerateWallpaper(s);
				_picBox.Image = genI;
			} catch (Exception ex) {
				displayError(ex);
			}
		}

		private void _colorButton1_Click(object sender, EventArgs e) {
			_color1 = SetColor(_color1, _colorButton1);
		}

		private void _colorButton2_Click(object sender, EventArgs e) {
			_color2 = SetColor(_color2, _colorButton2);
		}


		private void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			this.Visible = !this.Visible;
		}

		private void Form1_SizeChanged(object sender, EventArgs e) {
			if (this.WindowState == FormWindowState.Minimized) {
				this.Visible = false;
			}
		}

		#endregion

		#region Helpers
		private void displayError(Exception ex) {
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private Color SetColor(Color c, Button cb) {
			_colorDialog.Color = c;
			DialogResult dr = _colorDialog.ShowDialog();
			if (dr == DialogResult.OK) {
				c = _colorDialog.Color;
				cb.BackColor = c;
			}
			return c;
		}

		private Image GenerateWallpaper() {
			Size[] s = Screen.AllScreens.ToSizes();
			return GenerateWallpaper(s);
		}

		private Image GenerateWallpaper(Size[] s) {
			PaneledImageBuilder ib = new PaneledImageBuilder();
			Color[] c = getColors();
			Image[] i = getImages();

			ib.SetSizes(s);
			ib.SetColors(c);
			ib.SetImages(i);

			Image genI = ib.BuildImage();
			return genI;
		}

		private Image[] getImages() {
			string p1 = _wpPath1.Text;
			string p2 = _wpPath2.Text;

			Image i1 = Image.FromFile(p1);
			Image i2 = Image.FromFile(p2);

			return new Image[] { i1, i2 };
		}

		private Color[] getColors() {
			return new Color[] { _color1, _color2 };
		}

		private Size[] getSizes() {
			Size s1 = new Size((int)_xNud1.Value, (int)_yNud1.Value);
			Size s2 = new Size((int)_xNud2.Value, (int)_yNud2.Value);

			return new Size[] { s1, s2 };
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

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveConfig();
		}

		private void SaveConfig() {
			WallpaperConfigManager wcm = new WallpaperConfigManager();
			WallpaperConfigCollection cfg = multiWallpaperPicker1.Config;

			//wcm.Save(cfg);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			LoadConfig();
		}

		private void LoadConfig() {
			//WallpaperConfigManager wcm = new WallpaperConfigManager();
			//WallpaperConfigCollection cfg = wcm.Load();
			//multiWallpaperPicker1.Config = cfg;
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
			// Remove the icon from the notification area before closing
			_notifyIcon.Visible = false;

			SaveConfig();
		}

		private void TestForm_Load(object sender, EventArgs e) {
			timespanPicker1.TimeSpan = new TimeSpan(2, 22, 34, 2);
		}

	}
}