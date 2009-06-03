using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Resolution {
	public class Resolution {

		private const int MinWidth = 800;
		private const int MinHeight = 600;

		/// <summary>
		/// Name of the Display Device associated with the resolution
		/// </summary>
		public string DeviceName { get; set; }
		/// <summary>
		/// Width in pixels
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Height in pixels
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode.
		/// <para>This value is also known as the display device's vertical refresh rate.</para>
		/// <para>Default value: 60</para>
		/// </summary>
		public int RefreshRate { get; set; }
		/// <summary>
		/// Specifies the color resolution in bits per pixel
		/// <para>(for example: 4 bits for 16 colors, 8 bits for 256 colors, or 16 bits for 65,536 colors)</para>
		/// <para>Default Value: 32</para>
		/// </summary>
		public int BitsPerPixel { get; set; }
		/// <summary>
		/// X coordinate of top-left pixel relative to Primary Screen
		/// <para>Primary Screen is at location 0,0 </para>
		/// </summary>
		public int X { get; set; }
		/// <summary>
		/// Y coordinate of top-left pixel relative to Primary Screen
		/// <para>Primary Screen is at location 0,0 </para>
		/// </summary>
		public int Y { get; set; }
		/// <summary>
		/// Gets the bounds of the resolutions (X, Y, Width, Height)
		/// </summary>
		public Rectangle Bounds {
			get {
				return new Rectangle(X, Y, Width, Height);
			}
		}

		public Resolution(int width, int height) {
			Width = width;
			Height = height;
			RefreshRate = 60;
			BitsPerPixel = 32;
		}
		public Resolution(int width, int height, int x, int y)
			: this(width, height) {
			X = x;
			Y = y;
		}
		public Resolution(int width, int height, int x, int y, int refreshRate)
			: this(width, height, x, y) {
			RefreshRate = refreshRate;
		}
		public Resolution(int width, int height, int x, int y, int refreshrate, int bitsPerPixel)
			: this(width, height, x, y, refreshrate) {
			BitsPerPixel = bitsPerPixel;
		}
		public Resolution(DEVMODE devMode) {
			X = devMode.dmPositionX;
			Y = devMode.dmPositionY;
			Width = devMode.dmPelsWidth;
			Height = devMode.dmPelsHeight;
			BitsPerPixel = devMode.dmBitsPerPel;
			RefreshRate = devMode.dmDisplayFrequency;
			DeviceName = devMode.dmDeviceName;
		}


		public static Resolution[] GetAvailableResolutions(Screen scr) {
			return GetAvailableResolutions(scr, 32, 60);
		}
		public static Resolution[] GetAvailableResolutions(Screen scr, int bitsPerPixel) {
			return GetAvailableResolutions(scr, bitsPerPixel, 60);
		}
		public static Resolution[] GetAvailableResolutions(Screen scr, int bitsPerPixel, int refreshRate) {
			List<Resolution> resolutions = new List<Resolution>();
			DEVMODE dm = DEVMODE.Default;
			int mode = 0;
			while (0 != User32.EnumDisplaySettingsEx(
				scr.DeviceName, mode, ref dm, User32.EDS_RAWMODE)) {
				Size s = new Size(dm.dmPelsWidth, dm.dmPelsHeight);
				if (dm.dmPelsHeight >= MinHeight && dm.dmPelsWidth >= MinWidth &&
					dm.dmBitsPerPel == bitsPerPixel && dm.dmDisplayFrequency == refreshRate) {
					resolutions.Add(new Resolution(dm));
				}
				dm = DEVMODE.Default;
				mode++;
			}
			return resolutions.ToArray();
		}
		public static DISPLAY_DEVICE[] GetCurrentDisplayDevies() {
			DISPLAY_DEVICE dd = DISPLAY_DEVICE.Default;
			List<DISPLAY_DEVICE> devices = new List<DISPLAY_DEVICE>();
			try {
				for (int i = 0; User32.EnumDisplayDevices(null, (uint)i, ref dd, 0); i++) {
					if ((dd.StateFlags & User32.DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) == User32.DISPLAY_DEVICE_ATTACHED_TO_DESKTOP)
						devices.Add(dd);
					dd = DISPLAY_DEVICE.Default;
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			return devices.ToArray();
		}
		public static Resolution[] GetCurrentResolutions() {
			DEVMODE dm;
			DISPLAY_DEVICE[] monitors = GetCurrentDisplayDevies();
			List<Resolution> resolutions = new List<Resolution>();
			foreach (Screen s in Screen.AllScreens) {
				dm = DEVMODE.Default;
				if (0 != User32.EnumDisplaySettings(s.DeviceName, User32.ENUM_CURRENT_SETTINGS, ref dm)) {
					resolutions.Add(new Resolution(dm));
				}
			}
			return resolutions.ToArray();
		}
	}
}
