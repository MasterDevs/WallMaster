using System;
using System.IO;
using System.Xml.Serialization;

namespace WallpaperUtils {

	/// <summary>
	/// Class responsible for reading/writing configuration
	/// </summary>
	public class WallpaperConfigManager : ConfigManager<WallpaperChangerConfig> {
		private const string APP_FOLDER = @"Wallmaster";
		private const string APP_CONFIG = @"WallmasterConfig.xml";
		private const string WP_FILE = @"wallpaper.bmp";
		private const string PATH_FRMT = @"{0}\{1}";

		protected override string GetConfigPath() {
			return getFilePath(APP_CONFIG);
		}

		private string getFilePath(string fileName) {
			string dir = GetAppDir();
			string path = string.Format(PATH_FRMT, dir, fileName);

			return path;
		}

		private string GetAppDir() {
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			string path = string.Format(PATH_FRMT, appData, APP_FOLDER);
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}
			return path;
		}

		public string GetWallpaperPath() {
			return getFilePath(WP_FILE);
		}



		public WallpaperChangerConfig CreateEmptyConfig(int screenCount) {
			WallpaperChangerConfig cfg = WallpaperChangerConfig.GetDefault(screenCount);
			return cfg;
		}
	}


	public abstract class ConfigManager<T> {

		protected abstract string GetConfigPath();

		#region Load
		public T Load() {
			string p = GetConfigPath();
			return Load(p);
		}

		protected T Load(string path) {
			T cfg = default(T);

			// DOn't try to open a non-existent file.  Just return null.
			if (File.Exists(path)) {
				XmlSerializer xs = new XmlSerializer(typeof(T));
				using (StreamReader sr = new StreamReader(path)) {
					cfg = (T)xs.Deserialize(sr);
				}
			}
			return cfg;
		}

		#endregion

		#region Save

		public void Save(T cfg) {
			string p = GetConfigPath();
			SerializeAndSave(cfg, p);
		}

		protected void SerializeAndSave(T cfg, string path) {
			XmlSerializer xs = new XmlSerializer(typeof(T));
			using (StreamWriter sw = new StreamWriter(path)) {
				xs.Serialize(sw, cfg);
			}
		}

		#endregion
	}
}