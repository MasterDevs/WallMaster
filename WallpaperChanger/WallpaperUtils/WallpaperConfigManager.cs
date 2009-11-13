using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace WallpaperUtils {

	/// <summary>
	/// Class responsible for reading/writing configuration
	/// </summary>
	public class WallpaperConfigManager {

		private const string APP_FOLDER = @"Wallmaster";
		private const string CONFIG_FILE = @"WallmasterConfig.xml";
		private const string WP_FILE = @"wallpaper.bmp";
		private const string PATH_FRMT = @"{0}\{1}";

		private static string GetConfigPath() {
			return GetFilePath(CONFIG_FILE);
		}

		private static string GetFilePath(string fileName) {
			return string.Format(PATH_FRMT, AppDir, fileName);
		}

		private static string AppDir {
			get {
				string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

				string path = string.Format(PATH_FRMT, appData, APP_FOLDER);
				if (!Directory.Exists(path)) {
					Directory.CreateDirectory(path);
				}
				return path;
			}
		}

		/// <summary>
		/// Gets the path where the wallpaper should be saved.
		/// </summary>
		public static string WallpaperPath { get { return GetFilePath(WP_FILE); } }

		public static string ConfigPath { get { return GetFilePath(CONFIG_FILE); } }

		public static WallpaperConfigCollection CreateEmptyConfig(int screenCount) {
			WallpaperConfigCollection cfg = WallpaperConfigCollection.GetDefault(screenCount);
			return cfg;
		}

		#region Load
		/// <summary>
		/// Loads a wallpaper configuration collection from 
		/// the default location [ApplicationData\WallMaster\WallmasterConfig.xml]
		/// </summary>
		/// <returns>Returns WallpaperConfigCollection if config file found. Otherwise
		/// NULL is returned</returns>
		public static WallpaperConfigCollection Load() {
			string p = GetConfigPath();
			return Load(p);
		}
		
		/// <summary>
		/// Loads a wallpaper configuration collection from 
		/// the a specified location
		/// </summary>
		/// <returns>Returns WallpaperConfigCollection if config file found. Otherwise
		/// it'll return a default configuration based on the current number of screens.</returns>
		public static WallpaperConfigCollection Load(string path) {
			try {
				// Don't try to open a non-existent file.  Just return null.
				if (File.Exists(path)) {
					XmlSerializer xs = new XmlSerializer(typeof(WallpaperConfigCollection));
					using (StreamReader sr = new StreamReader(path)) {
						return (WallpaperConfigCollection)xs.Deserialize(sr);
					}
				}
			} catch (Exception) {}
			//-- If we can't read the configuration file, simply return null
			return null;
		}
		#endregion

		#region Save

		public static void Save(WallpaperConfigCollection config) {
			string p = GetConfigPath();
			SerializeAndSave(config, p);
		}

		protected static void SerializeAndSave(WallpaperConfigCollection config, string path) {
			XmlSerializer xs = new XmlSerializer(typeof(WallpaperConfigCollection));
			using (StreamWriter sw = new StreamWriter(path)) {
				xs.Serialize(sw, config);
			}
		}

		#endregion
	}
}