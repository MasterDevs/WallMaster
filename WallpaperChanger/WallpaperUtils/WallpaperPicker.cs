using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WallpaperUtils {
	public partial class WallpaperPicker : UserControl {

		#region Constants and Defaults
		private readonly Color DEFAULT_COLOR = Color.Black;
		private const string FILE_SEARCH_PATTERN_STRING = @"All Images|*.bmp;*.jpg;*.jpeg;*.gif;*.png|Bitmap (*.bmp)|*.bmp |JPEG (*.jpg)|*.jpg;*.jpeg |GIF (*.gif)|*.gif |PNG (*.png)|*.png";

		#endregion


		#region Helper Structs

		#region TimeSpanWrapper
		public struct TimeSpanWrapper {
			public TimeSpan Value;
			public string Name;
			public TimeSpanWrapper(string name, TimeSpan ts) {
				Value = ts;
				Name = name;
			}
			public override string ToString() {
				return Name;
			}
		}
		private TimeSpanWrapper[] TimeSpans = new TimeSpanWrapper[] {
			new TimeSpanWrapper("10 seconds", new TimeSpan(0, 0, 10)),
			new TimeSpanWrapper("30 seconds", new TimeSpan(0, 0, 30)),
			new TimeSpanWrapper("1 minute", new TimeSpan(0, 1, 0)),
			new TimeSpanWrapper("5 minutes", new TimeSpan(0, 5, 0)),
			new TimeSpanWrapper("10 minutes", new TimeSpan(0, 10, 0)),
			new TimeSpanWrapper("20 minutes", new TimeSpan(0, 20, 0)),
			new TimeSpanWrapper("30 minutes", new TimeSpan(0, 30, 0)),
			new TimeSpanWrapper("1 hour", new TimeSpan(1, 0, 0)),
			new TimeSpanWrapper("2 hours", new TimeSpan(2, 0, 0)),
			new TimeSpanWrapper("3 hours", new TimeSpan(3, 0, 0)),
			new TimeSpanWrapper("4 hours", new TimeSpan(4, 0, 0)),
			new TimeSpanWrapper("6 hours", new TimeSpan(6, 0, 0)),
			new TimeSpanWrapper("12 hours", new TimeSpan(12, 0, 0)),
			new TimeSpanWrapper("1 day", new TimeSpan(1, 0, 0, 0)),
			new TimeSpanWrapper("1 week", new TimeSpan(7, 0, 0, 0)) }; 
		#endregion

		#region WallpaperStretchStyleWrapper
		public struct WallpaperStretchStyleWrapper {
			public WallpaperStretchStyle Style;
			public string Name;
			public WallpaperStretchStyleWrapper(string name, WallpaperStretchStyle style) {
				Name = name;
				Style = style;
			}
			public override string ToString() {
				return Name;
			}
		}
		private WallpaperStretchStyleWrapper[] WallpaperStretchStyles =
			new WallpaperStretchStyleWrapper[] {
				new WallpaperStretchStyleWrapper("Center", WallpaperStretchStyle.Center),
				new WallpaperStretchStyleWrapper("Center Fit", WallpaperStretchStyle.CenterFit),
				new WallpaperStretchStyleWrapper("Fit", WallpaperStretchStyle.Fit),
				new WallpaperStretchStyleWrapper("Fill", WallpaperStretchStyle.Fill),
				new WallpaperStretchStyleWrapper("Stretch", WallpaperStretchStyle.Stretch) };
		#endregion

		#endregion

		private WallpaperConfig _cfg;
		public bool RaiseEvents { get; set; }

		public WallpaperConfig Config {
			get { return _cfg; }
			set {
				_cfg = value;
				SetFields();
			}
		}

		public event EventHandler<ConfigChangedEventArgs> ConfigChanged;


		#region CTOR

		public WallpaperPicker() {
			RaiseEvents = false;
			InitializeComponent();
			_cfg = new WallpaperConfig();
			_cfg.BackgroundColor = DEFAULT_COLOR;
			openFileDialog1.Filter = FILE_SEARCH_PATTERN_STRING;
			InitStretchStyle();
			RaiseEvents = true;

			InitComboBox();
		}

		private void InitComboBox() {
			_intervalComboBox.Items.Clear();
			foreach (TimeSpanWrapper pts in TimeSpans) {
				_intervalComboBox.Items.Add(pts);
			}
			_intervalComboBox.SelectedIndex = 0;
		}

		#endregion

		private void InitStretchStyle() {
			_stretchStyleCB.Items.Clear();

			foreach (WallpaperStretchStyleWrapper wr in WallpaperStretchStyles) {
				_stretchStyleCB.Items.Add(wr);
			}

			_stretchStyleCB.SelectedIndex = 0;
		}


		private void raiseConfigChanged() {
			if (ConfigChanged != null && RaiseEvents) {
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
			Config.ImagePath = string.Empty;
			raiseConfigChanged();
		}


		private void _imageRB_CheckedChanged(object sender, EventArgs e) {
			setImageSelectionMethod(WallpaperSelectionStyle.File);
			Config.ImagePath = _imagePathTB.Text;
			raiseConfigChanged();
		}

		private void _randomImageRB_CheckedChanged(object sender, EventArgs e) {
			setImageSelectionMethod(WallpaperSelectionStyle.Random);
			Config.DirectoryPath = _randomDirTB.Text;
			Config.ImagePath = _imagePathTB.Text;
			_intervalComboBox_SelectedIndexChanged(sender, e);
		}





		private void _imagePathTB_TextChanged(object sender, EventArgs e) {
			_cfg.ImagePath = _imagePathTB.Text;
			raiseConfigChanged();
		}

		private void _randomDirTB_TextChanged(object sender, EventArgs e) {
			_cfg.DirectoryPath = _randomDirTB.Text;
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
				changeRandomImage(false);
				raiseConfigChanged();
			}
		}

		#endregion


		#region Helper Methods
		private void displayError(Exception ex) {
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// Initializes the child controls values to reflect
		/// the current state of the configuration
		/// </summary>
		public void SetFields() {
			//-- To ensure it's thread safe as this may be called from
			// a timer event on the Changer class.
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(SetFieldsSafe));
			else
				SetFieldsSafe();
		}

		private void SetFieldsSafe() {
			_imagePathTB.Text = _cfg.ImagePath;
			_randomDirTB.Text = _cfg.DirectoryPath;
			setRadioButtons(_cfg.SelectionStyle);
			_colorButton.BackColor = _cfg.BackgroundColor;
			_includeSubdirsCB.Checked = _cfg.IncludeSubDirs;
			_intervalComboBox.SelectedItem = GetPossibleTimeSpan(_cfg.ChangeWallpaperInterval);
			setStretchStyle();
		}

		private void setStretchStyle() {
			foreach (WallpaperStretchStyleWrapper wr in WallpaperStretchStyles) {
				if (wr.Style == _cfg.StretchStyle)
					_stretchStyleCB.SelectedItem = wr;
			}
		}

		private object GetPossibleTimeSpan(TimeSpan timeSpan) {
			foreach (TimeSpanWrapper pts in TimeSpans) {
				if (pts.Value.Ticks == timeSpan.Ticks)
					return pts;
			}

			//-- Default 
			return TimeSpans[0];
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
			_intervalComboBox.Enabled = enabled;
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
				_imagePathTB.Text = _cfg.ImagePath;
			} catch (Exception e){
				displayError(e);
			}

			if (shouldRaiseChangeEvent) {
				raiseConfigChanged();
			}
		}

		public void ChangeRandom() {
			if (_cfg.IsRandom) {
				changeRandomImage(true);
			}
		}

		internal void ResetFields() {
			SetFields();
		}

		private void _includeSubdirsCB_CheckedChanged(object sender, EventArgs e) {
			_cfg.IncludeSubDirs = _includeSubdirsCB.Checked;
			raiseConfigChanged();
		}

		private void _stretchStyleCB_SelectedIndexChanged(object sender, EventArgs e) {
			_cfg.StretchStyle = ((WallpaperStretchStyleWrapper)_stretchStyleCB.SelectedItem).Style;
			raiseConfigChanged();
		}

		private void _intervalComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			_cfg.ChangeWallpaperInterval = ((TimeSpanWrapper)_intervalComboBox.SelectedItem).Value;
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
