using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WallpaperUtils {
	public partial class WallpaperPicker : UserControl {

		#region Constants and Defaults
		private readonly Color DEFAULT_COLOR = Color.Black;
		private const string FILE_SEARCH_PATTERN_STRING = @"All Images|*.bmp; *.jpg;*.jpeg; *.gif; *.png |Bitmap (*.bmp)|*.bmp |JPEG (*.jpg)|*.jpg;*.jpeg |GIF (*.gif)|*.gif |PNG (*.png)|*.png";

		#endregion
		

		private WallpaperConfig _cfg;
		private bool _raiseEvents;

		public WallpaperConfig Config {
			get { return _cfg; }
			set {
				_cfg = value;
				setFields();
			}
		}

		public event EventHandler<ConfigChangedEventArgs> ConfigChanged;


		#region CTOR

		public WallpaperPicker() {
			_raiseEvents = false;
			InitializeComponent();
			_cfg = new WallpaperConfig();
			_cfg.BackgroundColor = DEFAULT_COLOR;
			openFileDialog1.Filter = FILE_SEARCH_PATTERN_STRING;
			InitStretchStyle();
			_raiseEvents = true;
		}

		#endregion

		private void InitStretchStyle() {
			_stretchStyleCB.Items.Clear();

			Array styles = Enum.GetValues(typeof(WallpaperStretchStyle));

			foreach (WallpaperStretchStyle s in styles) {
				_stretchStyleCB.Items.Add(s);
			}

			_stretchStyleCB.SelectedIndex = 0;
		}


		private void raiseConfigChanged() {
			if (ConfigChanged != null && _raiseEvents) {
				ConfigChangedEventArgs cce = new ConfigChangedEventArgs(_cfg);
				ConfigChanged(this, cce);
			}
		}


		#region Form Events

		private void _colorButton_Click(object sender, EventArgs e) {
			colorDialog1.Color = _cfg.BackgroundColor;
			DialogResult dr = colorDialog1.ShowDialog();

			if (dr == DialogResult.OK) {
				_cfg.BackgroundColor = colorDialog1.Color;
				_colorButton.BackColor = colorDialog1.Color;
				raiseConfigChanged();
			}
		}


		private void _noImageRB_CheckedChanged(object sender, EventArgs e) {
			setImageSelectionMethod(WallpaperSelectionStyle.None);
			raiseConfigChanged();
		}


		private void _imageRB_CheckedChanged(object sender, EventArgs e) {
			setImageSelectionMethod(WallpaperSelectionStyle.File);
			raiseConfigChanged();
		}

		private void _randomImageRB_CheckedChanged(object sender, EventArgs e) {
			setImageSelectionMethod(WallpaperSelectionStyle.Random);
			raiseConfigChanged();
		}





		private void _imagePathTB_TextChanged(object sender, EventArgs e) {
			_cfg.ImagePath = _imagePathTB.Text;
			raiseConfigChanged();
		}

		private void _randomDirTB_TextChanged(object sender, EventArgs e) {
			_cfg.DirectoryPath = _randomDirTB.Text;

			// We only need tot ge a random file if we are set to random and the directory exists
			// The user could just be typing in a directory
			if (_cfg.IsRandom && Directory.Exists(_cfg.DirectoryPath)) {
				changeRandomImage(false);
				raiseConfigChanged();
			}

		}

	

		private void _browseImageButton_Click(object sender, EventArgs e) {

			// Set the initial file in the dialog to the currently selected file
			if (!string.IsNullOrEmpty(_imagePathTB.Text)) {
				FileInfo fi = new FileInfo(_imagePathTB.Text);
				if (fi.Exists) {
					openFileDialog1.FileName = fi.FullName;
				}
			}

			DialogResult dr = openFileDialog1.ShowDialog();
			if (dr == DialogResult.OK) {
				_imagePathTB.Text = openFileDialog1.FileName;
				raiseConfigChanged();
			}
		}

		private void _browseDirButton_Click(object sender, EventArgs e) {
			// Set the initial path in the dialog to the currently selected path

			string startUp = null;
			if (!string.IsNullOrEmpty(_randomDirTB.Text) && Directory.Exists(_randomDirTB.Text)) {
				DirectoryInfo di = new DirectoryInfo(_randomDirTB.Text);
				if (di.Exists) {
					startUp = di.FullName;
				}
			} else {
				startUp = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			}

			folderBrowserDialog1.SelectedPath = startUp;

			DialogResult dr = folderBrowserDialog1.ShowDialog();
			if (dr == DialogResult.OK) {
				_randomDirTB.Text = folderBrowserDialog1.SelectedPath;
				raiseConfigChanged();
			}
		}

		#endregion


		#region Helper Methods
		private void displayError(Exception ex) {
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		
		private void setFields() {
			setRadioButtons(_cfg.SelectionStyle);
			_imagePathTB.Text = _cfg.ImagePath;
			_randomDirTB.Text = _cfg.DirectoryPath;
			_colorButton.BackColor = _cfg.BackgroundColor;
			_includeSubdirsCB.Checked = _cfg.IncludeSubDirs;

			_stretchStyleCB.SelectedItem = _cfg.StretchStyle;
		}

		private void setRadioButtons(WallpaperSelectionStyle style) {
			switch (style) {
				case WallpaperSelectionStyle.None:
					_noImageRB.Checked = true;
					break;
				case WallpaperSelectionStyle.File:
					_imageRB.Checked = true;
					break;
				case WallpaperSelectionStyle.Random:
					_randomImageRB.Checked = true;
					break;
			}
		}



		private void setImageSelectionMethod(WallpaperSelectionStyle style) {
			_cfg.SelectionStyle = style;

			switch (style) {
				case WallpaperSelectionStyle.None:
					setImageSelectionEnabled(false);
					setDirectorySelectionEnabled(false);
					break;

				case WallpaperSelectionStyle.File:
					setImageSelectionEnabled(true);
					setDirectorySelectionEnabled(false);
					break;

				case WallpaperSelectionStyle.Random:
					setImageSelectionEnabled(false);
					setDirectorySelectionEnabled(true);
					break;
			}
		}

		/// <summary>
		/// Enables or disables the fields appropriate to selecting a directory
		/// </summary>
		/// <param name="enabled"></param>
		private void setDirectorySelectionEnabled(bool enabled) {
			_randomDirTB.Enabled = enabled;
			_browseDirButton.Enabled = enabled;
			_changeImageButton.Enabled = enabled;
			_includeSubdirsCB.Enabled = enabled;
		}

		/// <summary>
		/// Enables or disables the fields appropriate to selecting a single image
		/// </summary>
		/// <param name="p"></param>
		private void setImageSelectionEnabled(bool enabled) {
			_imagePathTB.Enabled = enabled;
			_browseImageButton.Enabled = enabled;
		}

		#endregion

		private void _changeImageButton_Click(object sender, EventArgs e) {
			changeRandomImage(true);
		}



		/// <summary>
		/// Changes the random image on the form
		/// </summary>
		/// <param name="shouldRaiseChangeEvent">
		/// If set to true, the ConfigChanged event is raised, ohterwise it isn't.
		/// This is useful when you are calling this from a method that will raise 
		/// this event anyway and you don't need to raise it twice.
		/// </param>
		private void changeRandomImage(bool shouldRaiseChangeEvent) {
			try {
				_cfg.ChangeRandomImage();
			} catch (Exception e){
				displayError(e);
			}

			if (shouldRaiseChangeEvent) {
				raiseConfigChanged();
			}
		}

		public void ChangeRandom() {
			if (_cfg.IsRandom) {
				changeRandomImage(false);
			}
		}

		internal void ResetFields() {
			setFields();
		}

		private void _includeSubdirsCB_CheckedChanged(object sender, EventArgs e) {
			_cfg.IncludeSubDirs = _includeSubdirsCB.Checked;
			raiseConfigChanged();
		}

		private void _stretchStyleCB_KeyPress(object sender, KeyPressEventArgs e) {
			// Set e.Handled to true so the user cannot type anything in here
			// Only way I know of to make sure the user can't just type anything they want here
			e.Handled = true;
		}

		private void _stretchStyleCB_SelectedIndexChanged(object sender, EventArgs e) {
			_cfg.StretchStyle = (WallpaperStretchStyle)_stretchStyleCB.SelectedItem;
			raiseConfigChanged();
		}
	}

	public class ConfigChangedEventArgs : EventArgs {
		public ConfigChangedEventArgs(WallpaperConfig config) {
			_config = config;
		}
		
		private WallpaperConfig _config;

		public WallpaperConfig Config {
			get { return _config; }
			set { _config = value; }
		}
	}
}
