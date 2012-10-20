using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace WallpaperUtils
{
    public static class WallpaperManager
    {
        private const int SPI_SET_DESKWALLPAPER = 20;
        private const int SPIF_SEND_WININICHANGE = 0x02;
        private const int SPIF_UPDATE_INIFILE = 0x01;

        public static int SetWallpaper(string path)
        {
            return SetWallpaper(path, WallpaperStyle.MultiMon);
        }

        public static int SetWallpaper(string path, WallpaperStyle style)
        {
            setStyle(style);

            int r = SystemParametersInfo(SPI_SET_DESKWALLPAPER, 2, path,
                                SPIF_UPDATE_INIFILE | SPIF_SEND_WININICHANGE);

            return r;
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