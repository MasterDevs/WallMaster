using Ninject.Extensions.Logging;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger
{
    public class ProgramRunner
    {
        private readonly WallpaperConfigManager _configManager;
        private readonly WallpaperChangerForm _form;
        private readonly ILogger _logger;
        private readonly QuickChanger _quickChanger;
        private readonly WallpaperConfigChanger _wallpaperConfigChanger;

        public ProgramRunner(WallpaperChangerForm form, 
            WallpaperConfigChanger wallpaperConfigChanger, 
            QuickChanger quickChanger, 
            WallpaperConfigManager configManager, 
            ILogger logger)
        {
            _form = form;
            _wallpaperConfigChanger = wallpaperConfigChanger;
            _quickChanger = quickChanger;
            _configManager = configManager;
            _logger = logger;
        }

        public void Run()
        {
            _logger.Info("Application started");
            var settings = _configManager.Load();
            if (settings == null || !settings.LoadFormMinimized)
            {
                Application.Run(_form);
            }
            else
            {
                StartMinimized();
            }
            _logger.Info("Application closed");
        }

        private void StartMinimized()
        {
            _wallpaperConfigChanger.Start(); // If the user has random wallpapers, we need to begin changing
            Application.Run();
        }
    }
}