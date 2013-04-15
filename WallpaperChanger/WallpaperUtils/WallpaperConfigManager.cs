using System;
using System.IO;
using System.Xml.Serialization;

namespace WallpaperUtils
{
    /// <summary>
    /// Class responsible for reading/writing configuration
    /// </summary>
    public class WallpaperConfigManager
    {
        private const string CONFIG_FILE = @"WallmasterConfig.xml";

        private readonly string _configPath;

        public WallpaperConfigManager(string appDir)
        {
            AppDir = appDir;
            _configPath = Path.Combine(appDir, CONFIG_FILE);
        }

        public string AppDir { get; private set; }

        public WallpaperConfigCollection CreateEmptyConfig(int screenCount)
        {
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
        public WallpaperConfigCollection Load()
        {
            return Load(_configPath);
        }

        /// <summary>
        /// Loads a wallpaper configuration collection from
        /// the a specified location
        /// </summary>
        /// <returns>Returns WallpaperConfigCollection if config file found. Otherwise
        /// it'll return a default configuration based on the current number of screens.</returns>
        public WallpaperConfigCollection Load(string path)
        {
            try
            {
                // Don't try to open a non-existent file.  Just return null.
                if (File.Exists(path))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(WallpaperConfigCollection));
                    using (StreamReader sr = new StreamReader(path))
                    {
                        return (WallpaperConfigCollection)xs.Deserialize(sr);
                    }
                }
            }
            catch (Exception) { }

            //-- If we can't read the configuration file, simply return null
            return null;
        }

        #endregion Load

        #region Save

        public void Save(WallpaperConfigCollection config)
        {
            SerializeAndSave(config, _configPath);
        }

        protected void SerializeAndSave(WallpaperConfigCollection config, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(WallpaperConfigCollection));
            using (StreamWriter sw = new StreamWriter(path))
            {
                xs.Serialize(sw, config);
            }
        }

        #endregion Save
    }
}