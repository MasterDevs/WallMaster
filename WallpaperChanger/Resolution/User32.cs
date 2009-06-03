using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Resolution {
	public class User32 {
		[DllImport("user32.dll")]
		public static extern int 
			EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
		[DllImport("user32.dll")]
		public static extern int EnumDisplaySettingsEx(string lpszDeviceName, int iModeNum, 
			ref DEVMODE lpDevMode, uint dwFlags);
		
		[DllImport("user32.dll")]
		public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum,
			ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

		[DllImport("user32.dll")]
		public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

		public const int ENUM_CURRENT_SETTINGS = -1;
		public const int ENUM_REGISTRY_SETTINGS = -2;
		public const int CDS_UPDATEREGISTRY = 0x01;
		public const int CDS_TEST = 0x02;
		public const int DISP_CHANGE_SUCCESSFUL = 0;
		public const int DISP_CHANGE_RESTART = 1;
		public const int DISP_CHANGE_FAILED = -1;
		public const int EDS_RAWMODE = 0x02;
		public const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x01;
	}
}
