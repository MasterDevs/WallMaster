using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WallpaperUtils
{
    public partial class WallpaperPicker : UserControl
    {
        #region Constants and Defaults

        private const string FILE_SEARCH_PATTERN_STRING = @"All Images|*.bmp;*.jpg;*.jpeg;*.gif;*.png|Bitmap (*.bmp)|*.bmp |JPEG (*.jpg)|*.jpg;*.jpeg |GIF (*.gif)|*.gif |PNG (*.png)|*.png";
        private readonly Color DEFAULT_COLOR = Color.Black;

        #endregion

        #region Helper Structs

        #region TimeSpanWrapper

        private TimeSpanWrapper[] TimeSpans = new TimeSpanWrapper[] {
            new TimeSpanWrapper("5 Seconds", TimeSpan.FromSeconds(5)),
            new TimeSpanWrapper("10 Seconds", TimeSpan.FromSeconds(10)),
            new TimeSpanWrapper("15 Seconds", TimeSpan.FromSeconds(15)),
            new TimeSpanWrapper("30 Seconds", TimeSpan.FromSeconds(30)),
            new TimeSpanWrapper("1 minute", TimeSpan.FromMinutes(1)),
            new TimeSpanWrapper("5 minutes", TimeSpan.FromMinutes(5)),
            new TimeSpanWrapper("10 minutes", TimeSpan.FromMinutes(10)),
            new TimeSpanWrapper("20 minutes", TimeSpan.FromMinutes(20)),
            new TimeSpanWrapper("30 minutes", TimeSpan.FromMinutes(30)),
            new TimeSpanWrapper("1 hour", TimeSpan.FromHours(1)),
            new TimeSpanWrapper("2 hours", TimeSpan.FromHours(2)),
            new TimeSpanWrapper("3 hours", TimeSpan.FromHours(3)),
            new TimeSpanWrapper("4 hours", TimeSpan.FromHours(4)),
            new TimeSpanWrapper("6 hours", TimeSpan.FromHours(6)),
            new TimeSpanWrapper("12 hours", TimeSpan.FromHours(12)),
            new TimeSpanWrapper("1 day", TimeSpan.FromDays(1)),
            new TimeSpanWrapper("1 week", TimeSpan.FromDays(7)) };

        public struct TimeSpanWrapper
        {
            public string Name;
            public TimeSpan Value;

            public TimeSpanWrapper(string name, TimeSpan ts)
            {
                Value = ts;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        #endregion

        #region WallpaperStretchStyleWrapper

        private WallpaperStretchStyleWrapper[] WallpaperStretchStyles =
            new WallpaperStretchStyleWrapper[] {
                new WallpaperStretchStyleWrapper("Center", WallpaperStretchStyle.Center),
                new WallpaperStretchStyleWrapper("Center Fit", WallpaperStretchStyle.CenterFit),
                new WallpaperStretchStyleWrapper("Fit", WallpaperStretchStyle.Fit),
                new WallpaperStretchStyleWrapper("Fill", WallpaperStretchStyle.Fill),
                new WallpaperStretchStyleWrapper("Stretch", WallpaperStretchStyle.Stretch) };

        public struct WallpaperStretchStyleWrapper
        {
            public string Name;
            public WallpaperStretchStyle Style;

            public WallpaperStretchStyleWrapper(string name, WallpaperStretchStyle style)
            {
                Name = name;
                Style = style;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        #endregion

        #endregion

        private WallpaperConfig _cfg;

        public event EventHandler<ConfigChangedEventArgs> ConfigChanged;

        public WallpaperConfig Config
        {
            get { return _cfg; }
            set
            {
                _cfg = value;
                SetFields();
            }
        }

        public bool RaiseEvents { get; set; }

        #region CTOR

        public WallpaperPicker()
        {
            RaiseEvents = false;
            InitializeComponent();
            _cfg = new WallpaperConfig();
            _cfg.BackgroundColor = DEFAULT_COLOR;
            openFileDialog1.Filter = FILE_SEARCH_PATTERN_STRING;
            InitStretchStyle();
            RaiseEvents = true;

            InitComboBox();
        }

        private void InitComboBox()
        {
            _intervalComboBox.Items.Clear();
            foreach (TimeSpanWrapper pts in TimeSpans)
            {
                _intervalComboBox.Items.Add(pts);
            }

            int defaultSelectedIndex = 0;
            if (_intervalComboBox.Items.Count > 9)
            {
                // prefer half an hour over 5 seconds as the default.
                defaultSelectedIndex = 8;
            }
            _intervalComboBox.SelectedIndex = defaultSelectedIndex;
        }

        #endregion

        public void ChangeRandom()
        {
            if (_cfg.IsRandom)
            {
                changeRandomImage(true);
            }
        }

        internal void ResetFields()
        {
            SetFields();
        }

        private void _changeImageButton_Click(object sender, EventArgs e)
        {
            changeRandomImage(true);
        }

        private void _includeSubdirsCB_CheckedChanged(object sender, EventArgs e)
        {
            _cfg.IncludeSubDirs = _includeSubdirsCB.Checked;
            raiseConfigChanged();
        }

        private void _intervalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cfg.ChangeWallpaperInterval = ((TimeSpanWrapper)_intervalComboBox.SelectedItem).Value;
            raiseConfigChanged();
        }

        private void _stretchStyleCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cfg.StretchStyle = ((WallpaperStretchStyleWrapper)_stretchStyleCB.SelectedItem).Style;
            raiseConfigChanged();
        }

        /// <summary>
        /// Changes the random image on the form
        /// </summary>
        /// <param name="shouldRaiseChangeEvent">
        /// If set to true, the ConfigChanged event is raised, ohterwise it isn't.
        /// This is useful when you are calling this from a method that will raise
        /// this event anyway and you don't need to raise it twice.
        /// </param>
        private void changeRandomImage(bool shouldRaiseChangeEvent)
        {
            try
            {
                _cfg.ChangeRandomImage();
                _imagePathTB.Text = _cfg.ImagePath;
            }
            catch (Exception e)
            {
                displayError(e);
            }

            if (shouldRaiseChangeEvent)
            {
                raiseConfigChanged();
            }
        }

        private void InitStretchStyle()
        {
            _stretchStyleCB.Items.Clear();

            foreach (WallpaperStretchStyleWrapper wr in WallpaperStretchStyles)
            {
                _stretchStyleCB.Items.Add(wr);
            }

            _stretchStyleCB.SelectedIndex = 0;
        }

        private void raiseConfigChanged()
        {
            if (ConfigChanged != null && RaiseEvents)
            {
                ConfigChangedEventArgs cce = new ConfigChangedEventArgs(_cfg);
                ConfigChanged(this, cce);
            }
        }

        #region Form Events

        private void _browseDirButton_Click(object sender, EventArgs e)
        {
            // Set the initial path in the dialog to the currently selected path

            string startUp = null;
            if (!string.IsNullOrEmpty(_randomDirTB.Text) && Directory.Exists(_randomDirTB.Text))
            {
                DirectoryInfo di = new DirectoryInfo(_randomDirTB.Text);
                if (di.Exists)
                {
                    startUp = di.FullName;
                }
            }
            else
            {
                startUp = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }

            folderBrowserDialog1.SelectedPath = startUp;

            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _randomDirTB.Text = folderBrowserDialog1.SelectedPath;
                changeRandomImage(false);
                raiseConfigChanged();
            }
        }

        private void _browseImageButton_Click(object sender, EventArgs e)
        {
            // Set the initial file in the dialog to the currently selected file
            if (!string.IsNullOrEmpty(_imagePathTB.Text))
            {
                FileInfo fi = new FileInfo(_imagePathTB.Text);
                if (fi.Exists)
                {
                    openFileDialog1.FileName = fi.FullName;
                }
            }

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _imagePathTB.Text = openFileDialog1.FileName;
                raiseConfigChanged();
            }
        }

        private void _colorButton_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _cfg.BackgroundColor;
            DialogResult dr = colorDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                _cfg.BackgroundColor = colorDialog1.Color;
                _colorButton.BackColor = colorDialog1.Color;
                raiseConfigChanged();
            }
        }

        private void _imagePathTB_TextChanged(object sender, EventArgs e)
        {
            _cfg.ImagePath = _imagePathTB.Text;
            raiseConfigChanged();
        }

        private void _imageRB_CheckedChanged(object sender, EventArgs e)
        {
            setImageSelectionMethod(WallpaperSelectionStyle.File);
            Config.ImagePath = _imagePathTB.Text;
            raiseConfigChanged();
        }

        private void _noImageRB_CheckedChanged(object sender, EventArgs e)
        {
            setImageSelectionMethod(WallpaperSelectionStyle.None);
            Config.ImagePath = string.Empty;
            raiseConfigChanged();
        }

        private void _randomDirTB_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(_randomDirTB.Text))
            {
                _cfg.DirectoryPath = _randomDirTB.Text;
                raiseConfigChanged();
            }
        }

        private void _randomImageRB_CheckedChanged(object sender, EventArgs e)
        {
            setImageSelectionMethod(WallpaperSelectionStyle.Random);
            Config.DirectoryPath = _randomDirTB.Text;
            Config.ImagePath = _imagePathTB.Text;
            _intervalComboBox_SelectedIndexChanged(sender, e);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initializes the child controls values to reflect
        /// the current state of the configuration
        /// </summary>
        public void SetFields()
        {
            //-- To ensure it's thread safe as this may be called from
            // a timer event on the Changer class.
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(SetFieldsSafe));
            else
                SetFieldsSafe();
        }

        private void displayError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private object GetPossibleTimeSpan(TimeSpan timeSpan)
        {
            foreach (TimeSpanWrapper pts in TimeSpans)
            {
                if (pts.Value.Ticks == timeSpan.Ticks)
                    return pts;
            }

            //-- Default
            return TimeSpans[0];
        }

        /// <summary>
        /// Enables or disables the fields appropriate to selecting a directory
        /// </summary>
        /// <param name="enabled"></param>
        private void setDirectorySelectionEnabled(bool enabled)
        {
            _randomDirTB.Enabled = enabled;
            _browseDirButton.Enabled = enabled;
            _changeImageButton.Enabled = enabled;
            _intervalComboBox.Enabled = enabled;
            _includeSubdirsCB.Enabled = enabled;
        }

        private void SetFieldsSafe()
        {
            _imagePathTB.Text = _cfg.ImagePath;
            _randomDirTB.Text = _cfg.DirectoryPath;
            setRadioButtons(_cfg.SelectionStyle);
            _colorButton.BackColor = _cfg.BackgroundColor;
            _includeSubdirsCB.Checked = _cfg.IncludeSubDirs;
            _intervalComboBox.SelectedItem = GetPossibleTimeSpan(_cfg.ChangeWallpaperInterval);
            setStretchStyle();
        }

        /// <summary>
        /// Enables or disables the fields appropriate to selecting a single image
        /// </summary>
        /// <param name="p"></param>
        private void setImageSelectionEnabled(bool enabled)
        {
            _imagePathTB.Enabled = enabled;
            _browseImageButton.Enabled = enabled;
        }

        private void setImageSelectionMethod(WallpaperSelectionStyle style)
        {
            _cfg.SelectionStyle = style;

            switch (style)
            {
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

        private void setRadioButtons(WallpaperSelectionStyle style)
        {
            switch (style)
            {
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

        private void setStretchStyle()
        {
            foreach (WallpaperStretchStyleWrapper wr in WallpaperStretchStyles)
            {
                if (wr.Style == _cfg.StretchStyle)
                    _stretchStyleCB.SelectedItem = wr;
            }
        }

        #endregion
    }
}