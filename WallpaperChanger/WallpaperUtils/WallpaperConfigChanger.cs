using System;
using System.Collections.Generic;
using SysTimers = System.Timers;
using System.Windows.Forms;

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

		private static SysTimers.Timer TheTimer;
		/// <summary>
		/// Dictionary of ScreenIndexes to Interval Counts
		/// </summary>
		private static SortedList<double, List<int>> TimeIntervals;
		private static double Count;
		private static double GCD;
		static WallpaperConfigChanger() {
			
		}

		private static void ChangeWallpaper(object sender, SysTimers.ElapsedEventArgs e) {
			TheTimer.Stop(); //-- Stop Timer
			List<int> indexesToChange = new List<int>(TimeIntervals.Count);
			foreach (var time in TimeIntervals.Keys) {
				if (((Count * GCD) % time) == 0)
					indexesToChange.AddRange(TimeIntervals[time]);
			}

			QuickChanger.ChangeWallpaper(indexesToChange.ToArray());
			//-- Fire event notifying that a wallpaper has been changed
			if (WallpaperChanged != null && indexesToChange.Count > 0)
				WallpaperChanged(new WallpaperChangeEventArgs(WallpaperConfigManager.Load()));
			
			//-- Incriment count
			Count++;
			
			//-- Restart Timer
			TheTimer.Start();

			//-- Manually Force Garbage Collection to keep things tidy
			// This is especially usefull when the image change interval is 1 minute
			GC.Collect();
		}

		#region Public Methods
		public static void Start() {
			//-- Load Current Configuraiton
			WallpaperConfigCollection configs = WallpaperConfigManager.Load();
			if (configs == null)
				return; //-- Simply return if failed to load a configuration
			TimeIntervals = new SortedList<double, List<int>>(configs.Count);
			
			//-- Check if we have a random screen config
			bool NoRandomScreenConfig = true;
			foreach (var c in configs) {
				if (c.IsRandom && c.ScreenIndex < Screen.AllScreens.Length) {
					NoRandomScreenConfig = false;
					break;
				}
			}

			if (NoRandomScreenConfig)
				return;

			//-- Now we have the shortest interval so we can initialize the timer
			if (TheTimer != null)
				TheTimer.Dispose();
			TheTimer = new SysTimers.Timer(60000); //-- 1 Minute Intervals
			TheTimer.Elapsed += new SysTimers.ElapsedEventHandler(ChangeWallpaper);
			foreach (var c in configs) {
				if (c.IsRandom) {
					//-- Find out what the interval is, based on total seconds, and add it to the list
					if (TimeIntervals.ContainsKey(c.ChangeWallpaperInterval.TotalSeconds))
						TimeIntervals[c.ChangeWallpaperInterval.TotalSeconds].Add(c.ScreenIndex);
					else
						TimeIntervals[c.ChangeWallpaperInterval.TotalSeconds] = new List<int>() { c.ScreenIndex };
				}
			}

			//-- Calculate the GCD which will act as our only timer
			CalculateGCD();

			TheTimer.Interval = GCD * 1000;

			//-- Initialize Count
			Count = 1;

			//-- Start the Timer
			TheTimer.Start();
		}

		public static void Stop() {
			if (TheTimer != null) {
				TheTimer.Elapsed -= new SysTimers.ElapsedEventHandler(ChangeWallpaper);
				TheTimer.Dispose();
			}
		} 
		#endregion

		#region Helper Methods
		
		/// <summary>
		/// This method will grab the GCD for all screen indexes.
		/// </summary>
		private static void CalculateGCD() {

			//-- If all random configs are to be changed on same interval,
			// the GCD is the interval.
			if (TimeIntervals.Count == 1) {
				GCD = TimeIntervals.Keys[0];
				return;
			}

			GCD = 1;
			IList<double> times = TimeIntervals.Keys;
			for (int i = 0; i < times.Count - 1; i++) {
				double gcd_temp = GreatestCommonDenominator(times[i], times[i + 1]);
				if (gcd_temp > GCD && GCDWorksForAllTimes(gcd_temp))
					GCD = gcd_temp;
			}
		}

		/// <summary>
		/// Checks to see if the possible GCD works for all time values.
		/// If so, returns true, false otherwise.
		/// </summary>
		private static bool GCDWorksForAllTimes(double gcd_temp) {
			foreach (double time in TimeIntervals.Keys) {
				if (time % gcd_temp != 0)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Simple recursive way to find the GCD.
		/// </summary>
		private static double GreatestCommonDenominator(double a, double b) {
			if (a == 0)
				return b;
			else if (b == 0)
				return a;

			if (a > b)
				return GreatestCommonDenominator(a % b, b);
			else
				return GreatestCommonDenominator(a, b % a);
		}

		#endregion
	}
}
