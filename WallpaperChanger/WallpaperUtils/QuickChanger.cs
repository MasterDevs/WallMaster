﻿using System.Windows.Forms;
using System.Drawing.Imaging;
namespace WallpaperUtils {
	/// <summary>
	/// This class exists to provide quick access to some ruitine tasks such as:
	/// <para>Change Wallpaper [Scree 1, 2 or Both]</para>
	/// <para>Adjust for resolution change</para>
	/// </summary>
	public class QuickChanger {

		public enum QuickChangeResult {
			/// <summary>
			/// There was an error loading the configuration.
			/// </summary>
			CouldNotLoadConfiguration,
			/// <summary>
			/// Submitted screen index is either:
			/// <para>Less than 0</para>
			/// <para>Outside bounds of Screen Configurations</para>
			/// <para>Outside bounds of current screen count</para>
			/// </summary>
			InvalidScreenIndex,
			/// <summary>
			/// Default value.
			/// </summary>
			None,
			/// <summary>
			/// The configuration for the desired screen index
			/// is not set to random and therefore we don't have a 
			/// random file directory and can't change the wallpaper.
			/// <para>This will also be set when changing all wallpapers
			/// and no screen has a random path</para>
			/// </summary>
			NoRandomPathAssociatedWithScreenIndex,
			/// <summary>
			/// We have more active screens then the number of configurations.
			/// </summary>
			ScreenCountConfigurationCountMisMatch,
			/// <summary>
			/// Wallpaper quick change event executed successfully.
			/// </summary>
			Success
		}

		private static QuickChangeResult _result = QuickChangeResult.None;
		public static QuickChangeResult Result { get { return _result; } }

		#region Private Fields
		private static WallpaperCreator Creator;
		private static WallpaperConfigManager Loader_Saver = new WallpaperConfigManager();
		private static WallpaperChangerConfig Configuration;
		#endregion

		#region Public Methods

		#region Private Properties
		private static bool CouldNotLoadConfiguration {
			get {
				if (Configuration == null) {
					_result = QuickChangeResult.CouldNotLoadConfiguration;
					return true;
				} else return false;
			}
		}

		#endregion

		/// <summary>
		/// Changes the background image for a particular screen.
		/// </summary>
		/// <param name="screenIndex">Screen Index that you would like to change</param>
		/// <returns>True if the image was changed, false otherwise</returns>
		public static bool ChangeWallpaper(int screenIndex) {
			Configuration = Loader_Saver.Load();
			if (CouldNotLoadConfiguration || NotARandomConfig(screenIndex) || InValidScreenIndex(screenIndex) ) {
				return false; 
			}

			//-- If we've made it this far, we're ok to change the wallpaper
			Configuration.Screens[screenIndex].ChangeRandomImage();
			InitScreens(false);
			
			//-- Set the wallpaper
			SetWallpaperAndSave();

			//-- Success
			_result = QuickChangeResult.Success;
			return true;
		}

		/// <summary>
		/// Changes all background images for screens that's configuration is set to
		/// random. 
		/// </summary>
		public static void ChangeAllWallpapers() {
			Configuration = Loader_Saver.Load();
			if (CouldNotLoadConfiguration) {
				return;
			}

			//-- Change all of the wallpapers that are set as random images
			bool has_A_Random_Screen = InitScreens(0, Screen.AllScreens.Length, true);
	
			//-- If we don't have a random configuration, just return
			if (!has_A_Random_Screen) {
				_result = QuickChangeResult.NoRandomPathAssociatedWithScreenIndex;
				return;
			}

			//-- Set the wallpaper
			SetWallpaperAndSave();
			
			//-- Success
			_result = QuickChangeResult.Success;
		}

		/// <summary>
		/// This method will update all screen images to reflect
		/// a change in resolution.
		/// <para>If any screens are set to random, this will NOT change
		/// the screen. Please use ChangeWallpaper or ChangeAllWallpapers for that</para>
		/// </summary>
		public static void Update() {
			Configuration = Loader_Saver.Load();
			if (CouldNotLoadConfiguration) {
				return;
			}

			if (Screen.AllScreens.Length > Configuration.Screens.Count) {
				_result = QuickChangeResult.ScreenCountConfigurationCountMisMatch;
				return;
			}

			InitScreens(0, Screen.AllScreens.Length, false);
		}

		#endregion

		#region Helper Methods
		
		/// <summary>
		/// Helper method ensures that we dispose the Creator object
		/// before we instantiate a new one.
		/// </summary>
		private static void InitCreator() {
			if (Creator != null)
				Creator.Dispose();
			Creator = new WallpaperCreator();
		}

		/// <summary>
		/// Iterates all configurations from startIndex to endIndex.
		/// </summary>
		/// <param name="startIndex">Start Index in Configurations</param>
		/// <param name="endIndex">End Index in Configurations</param>
		/// <param name="change">If true and screen is set to random, this will change the image</param>
		/// <returns>True if there was a random screen, false otherwise</returns>
		private static bool InitScreens(int startIndex, int endIndex, bool change) {
			//-- Initialize the creator
			InitCreator();
			
			bool has_A_Random_Screen = false;
			//-- Change all of the wallpapers that are set as random images
			for (int i = startIndex; i < endIndex; i++) {
				if (Configuration.Screens[i].IsRandom && change) {
					Configuration.Screens[i].ChangeRandomImage();
					has_A_Random_Screen = true;
				}
				Creator.InitScreen(i, Configuration.Screens[i]);
			}
			return has_A_Random_Screen;
		}

		/// <summary>
		/// Iterates all configurations for each screen index
		/// </summary>
		/// <param name="change">True if you want to change any random screen</param>
		/// <returns>True if there was a random screen, false otherwise</returns>
		private static bool InitScreens(bool change) { return InitScreens(0, Screen.AllScreens.Length, change); }

		/// <summary>
		/// Checks if the screen index is between 0 and Screen Count - 1
		/// </summary>
		/// <returns>True if screenIndex is valid, false otherwise</returns>
		private static bool InValidScreenIndex(int screenIndex) {
			if (screenIndex < 0 || screenIndex > (Configuration.Screens.Count - 1) || screenIndex > (Screen.AllScreens.Length - 1)) {
				_result = QuickChangeResult.InvalidScreenIndex;
				return true; //-- Invalid Screen Index
			}
			return false; //-- Valid Index
		}

		/// <summary>
		/// Returns true if screen index is valid and refers to a random configuration
		/// </summary>
		private static bool NotARandomConfig(int screenIndex) {
			if (!Configuration.Screens[screenIndex].IsRandom) {
				_result = QuickChangeResult.NoRandomPathAssociatedWithScreenIndex;
				return true; //-- Selected screen doesn't have a random path associated with it.
			}
			return false;
		}

		/// <summary>
		/// Sets the wallpaper and saves the current configuration
		/// </summary>
		private static void SetWallpaperAndSave() {
			string path = Loader_Saver.GetWallpaperPath();
			Creator.DesktopBitmap.Save(path, ImageFormat.Bmp);
			WallpaperManager.SetWallpaper(path);

			//-- Save the configuration so we know what the current images are
			Loader_Saver.Save(Configuration);
		}
		
		#endregion

	}
}
