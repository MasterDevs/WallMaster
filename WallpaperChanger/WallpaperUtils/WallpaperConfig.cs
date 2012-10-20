﻿using System.Drawing;
using System.Xml.Serialization;
using System;
using System.Windows.Forms;


namespace WallpaperUtils {

	/// <summary>
	/// Determines how the application chooses a wallpaper
	/// </summary>
	public enum WallpaperSelectionStyle {

		/// <summary>
		/// No wallpaper, just background color
		/// </summary>
		None = 0,

		/// <summary>
		/// Use the image in the specified file
		/// </summary>
		File = 1,

		/// <summary>
		/// Use a randomly selected image from a specified directory
		/// </summary>
		Random = 2,
	}

	/// <summary>
	/// Determines whether or not an image is stretched/rotated/centered
	/// This is used when the PaneledImageBuilder to determine how to create
	/// the image.
	/// </summary>
	public enum WallpaperStretchStyle {
		
		/// <summary>
		/// Center image, if it is larger than the destination screen 
		/// then crop the image at the bounds of the screen
		/// </summary>
		Center = 0,

		/// <summary>
		/// Image is centered unless it is larger then the desktop.
		/// If it's larger then the desktop, it will be fit,
		/// preserving it's aspect ratio.
		/// </summary>
		CenterFit = 1,

		/// <summary>
		/// Stretch the image to fit the destination screen.
		/// <para>If the image is larger or smaller then the destination, it will 
		/// be resized so the entire image appears on the destination screen</para>
		/// </summary>
		Stretch = 2,

		/// <summary>
		/// If the image is larger than the size of the screen
		/// then resize it to fit; aspect ratio is maintained, 
		/// </summary>
		Fit = 3,

		/// <summary>
		/// If an image is smaller than the screen, then center it, 
		/// otherwise, StretchRatio it.
		/// </summary>
		Fill = 4,
	}

	/// <summary>
	/// The configuration for a single screen
	/// </summary>
	public class WallpaperConfig {

		private readonly string[] FILE_FILTERS = { ".bmp", ".jpg", "jpeg", ".gif", ".png" };
		private TimeSpan _ChangeWallpaperInterval;

		#region Public Properties

		public string Name { get; set; }

		public string DeviceName { get; set; }

		public int ScreenIndex { get; set; }

		[XmlIgnore]
		public TimeSpan ChangeWallpaperInterval {
			get { return _ChangeWallpaperInterval; }
			set { _ChangeWallpaperInterval = value; }
		}

		public long ChangeWallpaperIntervalTicks {
			get { return _ChangeWallpaperInterval.Ticks; }
			set { _ChangeWallpaperInterval = new TimeSpan(value); }
		}

		[XmlIgnore]
		public Color BackgroundColor { get; set; }

		/// <summary>
		/// This property is necessary for background color serialization process
		/// </summary>
		public int Argb {
			get { return BackgroundColor.ToArgb(); }
			set { BackgroundColor = Color.FromArgb(value); }
		}

		/// <summary>
		/// The path to the directory selected when using random image selection
		/// </summary>
		public string DirectoryPath { get; set; }

		/// <summary>
		/// The path to the selected image file
		/// </summary>
		public string ImagePath { get; set; }

		/// <summary>
		/// Determines how the application chooses a wallpaper
		/// </summary>
		public WallpaperSelectionStyle SelectionStyle { get; set; }

		/// <summary>
		/// Determines whether or not an image is stretched/rotated/centered
		/// </summary>
		public WallpaperStretchStyle StretchStyle { get; set; }

		public bool IsRandom {
			get { return SelectionStyle == WallpaperSelectionStyle.Random; }
		}

		public bool IsFile {
			get { return SelectionStyle == WallpaperSelectionStyle.File; }
		}

		public bool IsNone {
			get { return SelectionStyle == WallpaperSelectionStyle.None; }
		}

		public bool IncludeSubDirs { get; set; }

		#endregion

		public override string ToString() {
			if (string.IsNullOrEmpty(Name)) {
				return "<Unamed WallpaperConfig>";
			}
			return Name;
		}

		/// <summary>
		/// Attempts to load the current image from file
		/// </summary>
		internal Image GetImage() {
			switch (SelectionStyle) {
				case WallpaperSelectionStyle.File:
				case WallpaperSelectionStyle.Random:
					return getImage(ImagePath);
				case WallpaperSelectionStyle.None:
				default:
					return null;
			}
		}

		private Image getImage(string path) {
			try {
				Image i = Image.FromFile(path);
				return i;
			} catch {
				return null;
			}
		}

		public void ChangeRandomImage() {
			if (IsRandom) {
				try {
					RandomFileFinder _randFile = new RandomFileFinder(DirectoryPath, FILE_FILTERS, IncludeSubDirs);
					ImagePath = _randFile.Current;
				} catch {
					ImagePath = null;
					throw;
				}
			}
		}

		/// <summary>
		/// Gets a default configuration.
		/// </summary>
		/// <param name="screenIndex">Index of the screen that the config will be created for</param>
		/// <exception cref="ArgumentException">ArgumentException will be fired if an invalid
		/// screen index is entered</exception>
		/// <returns></returns>
		public static WallpaperConfig GetDefault(int screenIndex) {
			if (screenIndex > -1 && screenIndex < Screen.AllScreens.Length) {
				WallpaperConfig config = new WallpaperConfig();
				config.BackgroundColor = Color.Black;
				config.SelectionStyle = WallpaperSelectionStyle.None;
				config.StretchStyle = WallpaperStretchStyle.Fill;
				config.Name = string.Format("Screen {0}", screenIndex);
				//config.Name = Screen.AllScreens[screenIndex].DeviceName;
				config.ScreenIndex = screenIndex;
				return config;
			} else {
				throw new ArgumentException(
					string.Format("Screen index of {0} is invalid in current display setup", screenIndex));
			}
		}
	}
}