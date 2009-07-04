using System;
using System.Collections.Generic;
using System.Timers;

namespace WallpaperUtils {
	public class Changer {

		public static System.Threading.Mutex Simple_Changer_Mutex;
		public static event WallpaperChangerChanged WallpaperChanged;
		public delegate void WallpaperChangerChanged(WallpaperChangeEventArgs args);
		public class WallpaperChangeEventArgs : EventArgs {
			public WallpaperConfigCollection Config;
			public WallpaperChangeEventArgs(WallpaperConfigCollection config) {
				Config = config;
			}
		}

		private static Dictionary<Timer, WallpaperConfigCollection> Timers;
		static Changer() {
			_configurations = new WallpaperConfigCollection();
			Timers = new Dictionary<Timer, WallpaperConfigCollection>(); 
		}

		private static WallpaperConfigCollection _configurations;

		static void ChangeWallpaper(object sender, ElapsedEventArgs e) {
			Timer timer = (Timer)sender;
			if (Timers.ContainsKey(timer)) {
				timer.Stop();
				WallpaperConfigCollection config = Timers[timer];
				QuickChanger.ChangeWallpaper(config.GetScreenIndexes());
				//-- Fire event notifying that a wallpaper has been changed
				if (WallpaperChanged != null)
					WallpaperChanged(new WallpaperChangeEventArgs(WallpaperConfigManager.Load()));
				//-- Restart Timer
				timer.Start();
			}
		}

		/// <summary>
		/// This method disposes the old timers and creates a new set of timers
		/// </summary>
		private static void DisposeTimers() {
			foreach (Timer t in Timers.Keys) {
				//-- Detach Event
				t.Elapsed -= new ElapsedEventHandler(ChangeWallpaper);
				t.Dispose();
			}
			Timers = new Dictionary<Timer, WallpaperConfigCollection>();
		}

		public static void Start() {
			_configurations = WallpaperConfigManager.Load();
			
			//-- Set up timers for all configurations that are random
			foreach (WallpaperConfig config in _configurations) {
				if (config.IsRandom) {
					bool NotAdded = true;
					//-- Check if we already have a timer set for the same interval
					foreach (Timer t in Timers.Keys) {
						//-- If we have a match, add the config to this timer
						if (t.Interval == (double)config.ChangeWallpaperInterval.TotalMilliseconds) {
							Timers[t].Add(config);
							NotAdded = false;
							break;
						}
					}

					if (NotAdded) {
						Timer t = new Timer((double)config.ChangeWallpaperInterval.TotalMilliseconds);
						t.Elapsed += new ElapsedEventHandler(ChangeWallpaper);
						Timers.Add(t, new WallpaperConfigCollection(config));
					}
				}
			}

			//-- Start all of the timers
			foreach (Timer t in Timers.Keys) {
				t.Start();
			}
		}

		public static void Stop() {
			DisposeTimers();
		}
	}
}
