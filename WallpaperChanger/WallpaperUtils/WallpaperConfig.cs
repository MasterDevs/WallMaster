using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WallpaperUtils
{
    /// <summary>
    /// The configuration for a single screen
    /// </summary>
    public class WallpaperConfig
    {
        private readonly string[] FILE_FILTERS = { ".bmp", ".jpg", "jpeg", ".gif", ".png" };
        private TimeSpan _ChangeWallpaperInterval;

        #region Public Properties

        /// <summary>
        /// This property is necessary for background color serialization process
        /// </summary>
        public int Argb
        {
            get { return BackgroundColor.ToArgb(); }
            set { BackgroundColor = Color.FromArgb(value); }
        }

        [XmlIgnore]
        public Color BackgroundColor { get; set; }

        [XmlIgnore]
        public TimeSpan ChangeWallpaperInterval
        {
            get { return _ChangeWallpaperInterval; }
            set { _ChangeWallpaperInterval = value; }
        }

        public long ChangeWallpaperIntervalTicks
        {
            get { return _ChangeWallpaperInterval.Ticks; }
            set { _ChangeWallpaperInterval = new TimeSpan(value); }
        }

        public string DeviceName { get; set; }

        /// <summary>
        /// The path to the directory selected when using random image selection
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// The path to the selected image file
        /// </summary>
        public string ImagePath { get; set; }

        public bool IncludeSubDirs { get; set; }

        public bool IsFile
        {
            get { return SelectionStyle == WallpaperSelectionStyle.File; }
        }

        public bool IsNone
        {
            get { return SelectionStyle == WallpaperSelectionStyle.None; }
        }

        public bool IsRandom
        {
            get { return SelectionStyle == WallpaperSelectionStyle.Random; }
        }

        public string Name { get; set; }

        public int ScreenIndex { get; set; }

        /// <summary>
        /// Determines how the application chooses a wallpaper
        /// </summary>
        public WallpaperSelectionStyle SelectionStyle { get; set; }

        /// <summary>
        /// Determines whether or not an image is stretched/rotated/centered
        /// </summary>
        public WallpaperStretchStyle StretchStyle { get; set; }

        #endregion

        /// <summary>
        /// Gets a default configuration.
        /// </summary>
        /// <param name="screenIndex">Index of the screen that the config will be created for</param>
        /// <exception cref="ArgumentException">ArgumentException will be fired if an invalid
        /// screen index is entered</exception>
        /// <returns></returns>
        public static WallpaperConfig GetDefault(int screenIndex)
        {
            if (screenIndex > -1 && screenIndex < Screen.AllScreens.Length)
            {
                WallpaperConfig config = new WallpaperConfig();
                config.BackgroundColor = Color.Black;
                config.SelectionStyle = WallpaperSelectionStyle.None;
                config.StretchStyle = WallpaperStretchStyle.Fill;
                config.Name = string.Format("Screen {0}", screenIndex);

                //config.Name = Screen.AllScreens[screenIndex].DeviceName;
                config.ScreenIndex = screenIndex;
                return config;
            }
            else
            {
                throw new ArgumentException(
                    string.Format("Screen index of {0} is invalid in current display setup", screenIndex));
            }
        }

        public void ChangeRandomImage()
        {
            if (IsRandom)
            {
                try
                {
                    RandomFileFinder _randFile = new RandomFileFinder(DirectoryPath, FILE_FILTERS, IncludeSubDirs);
                    ImagePath = _randFile.Current;
                }
                catch
                {
                    ImagePath = null;
                    throw;
                }
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "<Unamed WallpaperConfig>";
            }
            return Name;
        }

        /// <summary>
        /// Attempts to load the current image from file
        /// </summary>
        internal Image GetImage()
        {
            switch (SelectionStyle)
            {
                case WallpaperSelectionStyle.File:
                case WallpaperSelectionStyle.Random:
                    return getImage(ImagePath);
                case WallpaperSelectionStyle.None:
                default:
                    return null;
            }
        }

        private Image getImage(string path)
        {
            try
            {
                Image i = Image.FromFile(path);
                return i;
            }
            catch
            {
                return null;
            }
        }
    }
}