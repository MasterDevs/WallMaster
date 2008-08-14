using System;
using System.Drawing;
using System.Windows.Forms;

namespace WallpaperUtils {
	public partial class MultiWallpaperPicker : UserControl {

		private WallpaperConfigCollection _cfgs;

		public WallpaperConfigCollection Config {
			get { return _cfgs; }
			set {
				_cfgs = value;
				setFields();
			}
		}

		private bool _raiseEvents;

		#region CTOR
		public MultiWallpaperPicker() {
			_raiseEvents = false;
			InitializeComponent();
			Config = new WallpaperConfigCollection();
			_raiseEvents = true;

		}
		#endregion


		#region Helpers

		private void setFields() {
			int originalCount = _screenComboBox.Items.Count;
			int selectedIndex = _screenComboBox.SelectedIndex;

			_screenComboBox.Items.Clear();
			if (_cfgs == null || _cfgs.Count == 0) {
				return;
			}

			foreach (WallpaperConfig cfg in _cfgs) {
				_screenComboBox.Items.Add(cfg);
			}

			// If we still have the same number of config objects, 
			// then reuse the selected index 
			if (originalCount != _screenComboBox.Items.Count) {
				selectedIndex = 0;
			}

			// Make sure we are in bounds
			if (selectedIndex >= _screenComboBox.Items.Count || selectedIndex < 0) {
				selectedIndex = 0;
			}

			_screenComboBox.SelectedIndex = selectedIndex;
			selectedIndexChanged();
		}

		private void SetTestConfig() {
			WallpaperConfig c1 = new WallpaperConfig();
			c1.SelectionStyle = WallpaperSelectionStyle.File;
			c1.ImagePath = @"c:\img.jpg";
			c1.BackgroundColor = Color.Red;

			WallpaperConfig c2 = new WallpaperConfig();
			c2.SelectionStyle = WallpaperSelectionStyle.Random;
			c2.SelectedImagePath = @"c:\img2.jpg";
			c2.DirectoryPath = @"c:\";
			c2.BackgroundColor = Color.RoyalBlue;

			WallpaperConfig c3 = new WallpaperConfig();
			c3.SelectionStyle = WallpaperSelectionStyle.None;
			c3.BackgroundColor = Color.Sienna;

			_cfgs = new WallpaperConfigCollection(c1, c2, c3);
		}


		#endregion


		#region Page Events

		private void _screenComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			_raiseEvents = false;
			selectedIndexChanged();
			_raiseEvents = true;
		}

		private void selectedIndexChanged() {
			WallpaperConfig wc = (WallpaperConfig)_screenComboBox.SelectedItem;
			wpPicker.Config = wc;
		}

		private void wpPicker_ConfigChanged(object sender, ConfigChangedEventArgs e) {
			raiseConfigChanged();
		}
		#endregion

		private void raiseConfigChanged() {
			if (ConfigChanged != null && _raiseEvents) {
				ConfigsChangedEventArgs cce = new ConfigsChangedEventArgs(_cfgs);
				ConfigChanged(this, cce);
			}
		}


		public event EventHandler<ConfigsChangedEventArgs> ConfigChanged;



		public void ChangeRandomImage() {
			_cfgs.ChangeRandomImage();
			wpPicker.ResetFields();
			raiseConfigChanged();
		}
	}

	public class ConfigsChangedEventArgs : EventArgs {

		public ConfigsChangedEventArgs(WallpaperConfigCollection configs) {
			_cfgs = configs;
		}

		private WallpaperConfigCollection _cfgs;

		public WallpaperConfigCollection Configs {
			get { return _cfgs; }
			set { _cfgs = value; }
		}
	}
}
