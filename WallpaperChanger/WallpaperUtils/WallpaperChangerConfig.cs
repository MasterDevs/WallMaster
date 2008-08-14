using System;
using System.Xml.Serialization;

namespace WallpaperUtils {
	/// <summary>
	/// The root configuration class for the project
	/// </summary>
	public class WallpaperChangerConfig {

		#region Private Fields

		private WallpaperConfigCollection _screens;
		private TimeSpan _refreshRate;
		private TimeSpan _cycleRate;
		#endregion

		public static WallpaperChangerConfig GetDefault (int screenCount){
			WallpaperChangerConfig cfg = new WallpaperChangerConfig();
			cfg._refreshRate = TimeSpan.FromSeconds(1);
			cfg._cycleRate = TimeSpan.FromHours(1);

			cfg._screens = WallpaperConfigCollection.GetDefault(screenCount);
			return cfg;
		}


		public Double CycleWallpaperRateMilliseconds {
			get { return _cycleRate.TotalMilliseconds; }
			set { _cycleRate = TimeSpan.FromMilliseconds(value); }
		}


		public Double RefreshRateMilliseconds {
			get { return _refreshRate.TotalMilliseconds; }
			set { _refreshRate = TimeSpan.FromMilliseconds(value); }
		}


		/// <summary>
		/// The rate at which the wallpapers should rotate
		/// </summary>
		/// <remarks>
		/// Timespans won't serialize, so ignore them here.
		/// This value is saved in the CycleWallpaperRateMilliseconds
		/// </remarks>
		[XmlIgnore]
		public TimeSpan CycleWallpaperRate {
			get { return _cycleRate; }
			set { _cycleRate = value; }
		}

		/// <summary>
		/// How often to look see if the screen has changed sizes
		/// </summary>
		/// <remarks>
		/// Timespans won't serialize, so ignore them here.
		/// This value is saved in the RefreshRateMilliseconds
		/// </remarks>		[XmlIgnore]
		[XmlIgnore]
		public TimeSpan RefreshRate {
			get { return _refreshRate; }
			set { _refreshRate = value; }
		}

		public WallpaperConfigCollection Screens {
			get { return _screens; }
			set { _screens = value; }
		}
	}
}