using Microsoft.Win32;
using Ninject.Extensions.Logging;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WallpaperUtils
{
    public class WallpaperManager
    {
        public WallpaperManager(ILogger logger)
        {
            _logger = logger;
        }

        private const int SPI_SET_DESKWALLPAPER = 20;
        private const int SPIF_SEND_WININICHANGE = 0x02;
        private const int SPIF_UPDATE_INIFILE = 0x01;
        private readonly ILogger _logger;

        public void SetWallpaper(string path)
        {
            SetWallpaper(path, WallpaperStyle.MultiMon);
        }

        public void SetWallpaper(string path, WallpaperStyle style)
        {
            setStyle(style);

            int success = SystemParametersInfo(SPI_SET_DESKWALLPAPER, 2, path,
                                SPIF_UPDATE_INIFILE | SPIF_SEND_WININICHANGE);

            if (success == 0)
            {
                int errorNumber = Marshal.GetLastWin32Error();
                var ex = new Win32Exception(errorNumber);
                if (ex.ErrorCode > 0) throw ex;
            }
        }

        private static void setStyle(WallpaperStyle style)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            switch (style)
            {
                case WallpaperStyle.Stretch:
                    key.SetValue(@"WallpaperStyle", "2");
                    key.SetValue(@"TileWallpaper", "0");
                    break;

                case WallpaperStyle.Center:
                    key.SetValue(@"WallpaperStyle", "1");
                    key.SetValue(@"TileWallpaper", "0");
                    break;

                case WallpaperStyle.Tile:
                    key.SetValue(@"WallpaperStyle", "1");
                    key.SetValue(@"TileWallpaper", "1");
                    break;

                case WallpaperStyle.MultiMon:
                    key.SetValue(@"WallpaperStyle", "0");
                    key.SetValue(@"TileWallpaper", "1");
                    break;
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction,
                                                       int uParam,
                                                       string lpvParam,
                                                       int fuWinIni);
    }
}