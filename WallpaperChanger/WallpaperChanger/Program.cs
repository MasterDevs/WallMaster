using System;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Exit();
			ParseArgs(args);
		}

		private static void ParseArgs(string[] args) {

			//-- Just run application if there are no arguments
			if(args.Length == 0)
				Application.Run(new SimpleTestForm());

			//-- Simple Arguments
			if (args.Length == 1) {
				string arg = args[0].ToLower();
				if (CheckArg(arg, "c"))				//-- Change All Wallpapers
					QuickChanger.ChangeAllWallpapers();
				else if (CheckArg(arg, "u"))	//-- Update Wallpaper for resolution change
					QuickChanger.Update();
				else return;
			}

			//-- Complex Arguments
			if (args.Length == 2) {
				string arg = args[0].ToLower();
				int index;
				if (CheckArg(arg, "c") && int.TryParse(args[1], out index)) {
					QuickChanger.ChangeWallpaper(index);
				} 
			}
		}

		/// <summary>
		/// This method will check for all argument varitions of a desired arg
		/// </summary>
		/// <param name="desiredArg">Just the charactor, no flags like '/' or '-' or '--'</param>
		/// <returns>True if inputted arg is the desired argument</returns>
		private static bool CheckArg(string inputedArg, string desiredArg) {
			return
				inputedArg == desiredArg ||
				inputedArg == string.Format("-{0}", desiredArg) ||
				inputedArg == string.Format("--{0}", desiredArg) ||
				inputedArg == string.Format("/{0}", desiredArg) ||
				inputedArg == string.Format(@"\{0}", desiredArg);
		}
	}
}
