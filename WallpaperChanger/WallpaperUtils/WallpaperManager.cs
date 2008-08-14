using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WallpaperUtils {
	public enum WallpaperStyle {
		Stretch = 0,
		Center = 1,
		Tile = 2,
		MultiMon = 3,
	}

	public class WallpaperManager {
		const int SPI_SET_DESKWALLPAPER = 20;
		const int SPIF_UPDATE_INIFILE = 0x01;
		const int SPIF_SEND_WININICHANGE = 0x02;


		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern int SystemParametersInfo(int uAction,
																						int uParam,
																						string lpvParam,
																						int fuWinIni);


		public static int SetWallpaper(string path) {
			return SetWallpaper(path, WallpaperStyle.MultiMon);
		}

		public static int SetWallpaper(string path, WallpaperStyle style) {

			setStyle(style);

			int r = SystemParametersInfo(SPI_SET_DESKWALLPAPER, 2, path,
								SPIF_UPDATE_INIFILE | SPIF_SEND_WININICHANGE);

			return r;
		}

		private static void setStyle(WallpaperStyle style) {
			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			switch (style) {
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


		public static Image CreateWP(Image[] images) {
			return null;
		}

		public static Image creatWP(Image image, int x, int y, Color c) {
			Bitmap bm = new Bitmap(x, y);
			Graphics g = Graphics.FromImage(bm);

			Brush br = new SolidBrush(c);

			g.FillRectangle(br, 0, 0, x, y);

			g.DrawImage(image, 20, 20);
			return bm;
		}
	}
}