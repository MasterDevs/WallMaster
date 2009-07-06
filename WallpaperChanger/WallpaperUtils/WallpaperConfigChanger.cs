using System;
using System.Collections.Generic;
using System.Timers;

namespace WallpaperUtils {
	/// <summary>
	/// This class is designed to have two public facing methods - Start & Stop
	/// When started, this class will call QuickChanger, updating the appropriate screens.
	/// If there are multiple wallpapers and events overlap, only one call will
	/// be made to QuickChanger.
	/// </summary>
	public class WallpaperConfigChanger {

		#region WallpaperChanged Event
		public static event WallpaperChangerChanged WallpaperChanged;
		public delegate void WallpaperChangerChanged(WallpaperChangeEventArgs args);
		public class WallpaperChangeEventArgs : EventArgs {
			public WallpaperConfigCollection Config;
			public WallpaperChangeEventArgs(WallpaperConfigCollection config) {
				Config = config;
			}
		} 
		#endregion

		private static Timer TheTimer;
		/// <summary>
		/// Dictionary of ScreenIndexes to Interval Counts
		/// </summary>
		private static Dictionary<int, double> TimeIntervals;
		private static double Count;
		static WallpaperConfigChanger() {
			
		}

		private static void ChangeWallpaper(object sender, ElapsedEventArgs e) {
			TheTimer.Stop(); //-- Stop Timer
			List<int> indexesToChange = new List<int>(TimeIntervals.Count);
			foreach (var screenIndex in TimeIntervals.Keys) {
				if ((Count % TimeIntervals[screenIndex]) == 0)
					indexesToChange.Add(screenIndex);
			}

			QuickChanger.ChangeWallpaper(indexesToChange.ToArray());
			//-- Fire event notifying that a wallpaper has been changed
			if (WallpaperChanged != null)
				WallpaperChanged(new WallpaperChangeEventArgs(WallpaperConfigManager.Load()));
			
			//-- Incriment count
			Count++;
			
			//-- Restart Timer
			TheTimer.Start();

			//-- Manually Force Garbage Collection to keep things tidy
			// This is especially usefull when the image change interval is
			// 10 seconds. 
			GC.Collect();
		}

		#region Public Methods
		public static void Start() {
			//-- Load Current Configuraiton
			WallpaperConfigCollection configs = WallpaperConfigManager.Load();
			if (configs == null)
				return; //-- Simply return if failed to load a configuration
			TimeIntervals = new Dictionary<int, double>(configs.Count);

			//-- Check if we have a random
			bool NoRandomWallpaper = true;

			//-- Find Random wallpaper with shortest random change time
			double min = double.MaxValue;
			foreach (var c in configs) {
				if (c.IsRandom) {
					min = Math.Min(c.ChangeWallpaperInterval.TotalMilliseconds, min);
					NoRandomWallpaper = false;
				}
			}

			if (NoRandomWallpaper)
				return;

			//-- Now we have the shortest interval so we can initialize the timer
			if (TheTimer != null)
				TheTimer.Dispose();
			TheTimer = new Timer(min);
			TheTimer.Elapsed += new ElapsedEventHandler(ChangeWallpaper);
			foreach (var c in configs) {
				if (c.IsRandom) {
					//-- Find out what the interval is, based on min, and add it to the list
					TimeIntervals.Add(c.ScreenIndex, c.ChangeWallpaperInterval.TotalMilliseconds / min);
				}
			}

			//-- Initialize Count
			Count = 1;

			//-- Start the Timer
			TheTimer.Start();
		}

		public static void Stop() {
			if (TheTimer != null) {
				TheTimer.Elapsed -= new ElapsedEventHandler(ChangeWallpaper);
				TheTimer.Dispose();
			}
		} 
		#endregion
	}
}
