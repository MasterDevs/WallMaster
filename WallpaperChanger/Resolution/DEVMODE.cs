using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Resolution {

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct DEVMODE  {
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmDeviceName;
		public short dmSpecVersion;
		public short dmDriverVersion;
		public short dmSize;
		public short dmDriverExtra;
		public int dmFields;
		public int dmPositionX;
		public int dmPositionY;
		public int dmDisplayOrientation;
		public int dmDisplayFixedOutput;
		public short dmColor;
		public short dmDuplex;
		public short dmYResolution;
		public short dmTTOption;
		public short dmCollate;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmFormName;

		public short dmLogPixels;
	  /// <summary>
	  /// Specifies the color resolution, in bits per pixel, of the display device 
	  /// <para>(for example: 4 bits for 16 colors, 8 bits for 256 colors, or 16 bits for 65,536 colors)</para>
	  /// </summary>
		public short dmBitsPerPel;
		/// <summary>
	  /// Specifies the width, in pixels, of the visible device surface.
	  /// </summary>
		public int dmPelsWidth;
	  /// <summary>
	  /// Specifies the height, in pixels, of the visible device surface.
	  /// </summary>
		public int dmPelsHeight;
		/// <summary>
	  /// Specifies the device's display mode.
	  /// This member can be a combination of the following values.
	  /// <para>DM_GRAYSCALE - Specifies that the display is a noncolor device. 
	  /// If this flag is not set, color is assumed.</para>
	  /// <para>DM_INTERLACED - Specifies that the display mode is interlaced. 
	  /// If the flag is not set, noninterlaced is assumed. </para>
	  /// </summary>
		public int dmDisplayFlags;
		/// <summary>
	  /// Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode. 
	  /// This value is also known as the display device's vertical refresh rate
	  /// </summary>
		public int dmDisplayFrequency;
		public int dmICMMethod;
		public int dmICMIntent;
		public int dmMediaType;
		public int dmDitherType;
		public int dmReserved1;
		public int dmReserved2;
		public int dmPanningWidth;
		public int dmPanningHeight;

		public static DEVMODE Default {
			get {
				DEVMODE dm = new DEVMODE();
				dm.dmDeviceName = new String(new char[32]);
				dm.dmFormName = new String(new char[32]);
				dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
				dm.dmDriverExtra = (short)0;
				return dm;
			}
		}
	};
}
