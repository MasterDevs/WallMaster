using Ninject.Extensions.Logging;
using System;
using System.IO;
using System.Windows.Forms;

namespace WallpaperUtils
{
    public class StartWithWindowsHelper
    {
        private const string LINK_FILE = "WallMaster.appref-ms";
        private readonly ILogger _logger;
        private readonly string _shortcutPath;

        public StartWithWindowsHelper(ILogger logger)
        {
            _logger = logger;
            _shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), LINK_FILE);
        }

        public bool StartWithWindowsEnabled
        {
            get { return File.Exists(_shortcutPath); }
            set
            {
                if (value)
                {
                    CreateShortcut();
                }
                else
                {
                    DeleteShortcut();
                }
            }
        }

        private void CreateShortcut()
        {
            try
            {
                if (StartWithWindowsEnabled) return;

                var fi = new FileInfo(Application.ExecutablePath).Directory;
                var linkSource = new FileInfo(Path.Combine(fi.ToString(), LINK_FILE));

                linkSource.CopyTo(_shortcutPath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "failed to create shortcut at {0}", _shortcutPath ?? "<path is null>");
            }
        }

        private void DeleteShortcut()
        {
            try
            {
                FileInfo f = new FileInfo(_shortcutPath);
                if (f.Exists) f.Delete();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "failed to delete shortcut at {0}", _shortcutPath ?? "<path is null>");
            }
        }
    }
}