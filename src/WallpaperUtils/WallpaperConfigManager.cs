using Ninject.Extensions.Logging;
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
        private readonly ILogger _logger;

        public WallpaperConfigManager(ILogger logger, string appDir)
        {
            AppDir = appDir;
            _configPath = Path.Combine(appDir, CONFIG_FILE);
            _logger = logger;
        }

        public string AppDir { get; private set; }

        #region Load

        /// <summary>
        /// Loads a wallpaper configuration collection from
        /// the default location [ApplicationData\WallMaster\WallmasterConfig.xml]
        /// </summary>
        /// <returns>Returns WallpaperConfigCollection if config file found. Otherwise null</returns>
        /// <remarks>First we try to load the new log file format <see cref="WallpaperSettings"/>.
        /// If the format is incorrect then we try again with the legacy format <see cref="WallpaperConfigCollection"/></remarks>
        public WallpaperSettings Load()
        {
            WallpaperSettings settings;
            if (!File.Exists(_configPath))
            {
                _logger.Warn("Configuration file does not exist.  Creating default configuration");
                var screenConfigs = WallpaperConfigCollection.GetDefault(WallpaperUtils.Screen.AllScreenCount);
                settings = new WallpaperSettings
                {
                    ScreenConfigs = screenConfigs,
                };
            }
            else
            {
                settings = Load<WallpaperSettings>(_configPath);
                if (settings == null)
                {
                    _logger.Warn("Could not load config file.  Attempting to load a legacy file format");
                    var screenConfigs = Load<WallpaperConfigCollection>(_configPath);

                    if (screenConfigs == null)
                    {
                        _logger.Warn("Could not load the config file as legacy.  Creating default configuration");
                        screenConfigs = WallpaperConfigCollection.GetDefault(WallpaperUtils.Screen.AllScreenCount);
                    }
                    if (screenConfigs != null)
                    {
                        settings = new WallpaperSettings
                        {
                            ScreenConfigs = screenConfigs
                        };
                    }
                }
            }

            return settings;
        }

        private T Load<T>(string path) where T : class
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamReader sr = new StreamReader(path))
                {
                    return (T)xs.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error loading log file {0}", path);
                return null;
            }
        }

        #endregion Load

        #region Save

        public void Save(WallpaperSettings settings)
        {
            SerializeAndSave(settings, _configPath);
        }

        protected void SerializeAndSave(WallpaperSettings settings, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(WallpaperSettings));
            using (StreamWriter sw = new StreamWriter(path))
            {
                xs.Serialize(sw, settings);
            }
        }

        #endregion Save
    }
}