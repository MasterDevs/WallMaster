using System.Drawing;
using System.Xml.Serialization;


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
		/// Center image, if it is larger than the size of the screen 
		/// then crop the image at the bounds of the screen
		/// </summary>
		Center = 0,

		/// <summary>
		/// If the image is larger than the size of the screen
		/// then resize it to fit; aspect ratio is NOT maintaned
		/// </summary>
		Stretch = 1,

		/// <summary>
		/// If the image is larger than the size of the screen
		/// then resize it to fit; aspect ratio is maintaned, 
		/// </summary>
		StretchRatio = 2, 

		/// <summary>
		/// The best of both worlds.
		/// If an image is smaller than the screen, then center it, 
		/// otherwise, StretchRatio it.
		/// </summary>
		Magic = 3,
	}

	/// <summary>
	/// The configuration for a single screen
	/// </summary>
	public class WallpaperConfig {

		private readonly string[] FILE_FILTERS = { ".bmp", ".jpg", "jpeg", ".gif", ".png" };

		#region Private Fields
		private WallpaperSelectionStyle _wpSelectionStyle;
		private WallpaperStretchStyle _wpStretchStyle;

		private string _selectedImagePath;
		private string _imagePath;
		private string _directoryPath;
		private string _name;
		private bool _includeSubDirs;

		private Color _backgroundColor;

		#endregion


		#region Public Properties

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		[XmlIgnore]
		public Color BackgroundColor {
			get { return _backgroundColor; }
			set { _backgroundColor = value; }
		}

		public int Argb {
			get { return _backgroundColor.ToArgb(); }
			set { _backgroundColor = Color.FromArgb(value); }
		}

		/// <summary>
		/// The path to the directory selected when using random image selection
		/// </summary>
		public string DirectoryPath {
			get { return _directoryPath; }
			set { _directoryPath = value; }
		}

		/// <summary>
		/// The path to the selected image file
		/// </summary>
		public string ImagePath {
			get { return _imagePath; }
			set { _imagePath = value; }
		}


		public string SelectedImagePath {
			get { return _selectedImagePath; }
			set { _selectedImagePath = value; }
		}

		/// <summary>
		/// Determines how the application chooses a wallpaper
		/// </summary>
		public WallpaperSelectionStyle SelectionStyle {
			get { return _wpSelectionStyle; }
			set { _wpSelectionStyle = value; }
		}

		/// <summary>
		/// Determines whether or not an image is stretched/rotated/centered
		/// </summary>
		public WallpaperStretchStyle StretchStyle {
			get { return _wpStretchStyle; }
			set { _wpStretchStyle = value; }
		}

		public bool IsRandom {
			get { return _wpSelectionStyle == WallpaperSelectionStyle.Random; }
		}

		public bool IsFile {
			get { return _wpSelectionStyle == WallpaperSelectionStyle.File; }
		}

		public bool IsNone {
			get { return _wpSelectionStyle == WallpaperSelectionStyle.None; }
		}

		public bool IncludeSubDirs {
			get { return _includeSubDirs; }
			set { _includeSubDirs = value; }
		}

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
		/// <remarks>
		/// If the current style is set to 
		///		Random:  loads from the _selectedImagePath
		///		File:  Loads from _imagePath
		///		None:  Returns null
		/// </remarks>
		/// <returns></returns>
		internal Image GetImage() {
			switch (_wpSelectionStyle) {
				case WallpaperSelectionStyle.File:
					return getImage(_imagePath);
				case WallpaperSelectionStyle.Random:
					return getImage(_selectedImagePath);
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
					RandomFileFinder _randFile = new RandomFileFinder(DirectoryPath, FILE_FILTERS, _includeSubDirs);
					SelectedImagePath = _randFile.Current;
				} catch {
					SelectedImagePath = null;
					throw;
				}
			}
		}

		public static WallpaperConfig GetDefault(int screenNumber) {
			WallpaperConfig config = new WallpaperConfig();
			config.BackgroundColor = Color.Black;
			config.SelectionStyle = WallpaperSelectionStyle.None;
			config.StretchStyle = WallpaperStretchStyle.Magic;
			config.Name = string.Format("Screen {0}", + screenNumber + 1);

			return config;
		}
	}
}