using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Resolution {
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct DISPLAY_DEVICE {
		[MarshalAs(UnmanagedType.U4)]
		public int cb;
		
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string DeviceName;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceString;

		[MarshalAs(UnmanagedType.U4)]
		public uint StateFlags;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceID;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceKey;

		public static DISPLAY_DEVICE Default {
			get {
				DISPLAY_DEVICE dd = new DISPLAY_DEVICE();
				dd.cb = Marshal.SizeOf(dd);
				return dd;
			}
		}
	}
}
