using System;

namespace WallpaperUtils
{
    public class ConfigChangedEventArgs : EventArgs
    {
        private WallpaperConfig _config;

        public ConfigChangedEventArgs(WallpaperConfig config)
        {
            _config = config;
        }

        public WallpaperConfig Config
        {
            get { return _config; }
            set { _config = value; }
        }
    }
}