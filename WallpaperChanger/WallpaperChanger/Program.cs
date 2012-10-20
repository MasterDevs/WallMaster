using System;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ParseArgs(args);
        }

        /// <summary>
        /// This method will check for all argument variations of a desired arg
        /// </summary>
        /// <param name="desiredArg">Just the character, no flags like '/' or '-' or '--'</param>
        /// <returns>True if inputted arg is the desired argument</returns>
        private static bool CheckArg(string inputedArg, string desiredArg)
        {
            return
                inputedArg == desiredArg ||
                inputedArg == string.Format("-{0}", desiredArg) ||
                inputedArg == string.Format("--{0}", desiredArg) ||
                inputedArg == string.Format("/{0}", desiredArg) ||
                inputedArg == string.Format(@"\{0}", desiredArg);
        }

        private static void ParseArgs(string[] args)
        {
            WallpaperChangerForm wcf;

            //-- Just run application if there are no arguments
            if (args.Length < 1)
                Application.Run(new WallpaperChangerForm());

            //-- Simple Arguments
            else if (args.Length < 2)
            {
                string arg = args[0].ToLower();
                if (CheckArg(arg, "c"))				//-- Change All Wallpapers
                    QuickChanger.ChangeAllWallpapers();
                else if (CheckArg(arg, "u"))	//-- Update Wallpaper for resolution change
                    QuickChanger.Update();
                else if (CheckArg(arg, "h"))
                { //-- Hidden Mode - Start Minimized to Tray
                    wcf = new WallpaperChangerForm();

                    //-- If the user has random wallpapers, we need to begin changing
                    WallpaperConfigChanger.Start();
                    Application.Run();
                }
            }

            //-- Complex Arguments
            else if (args.Length < 3)
            {
                string arg = args[0].ToLower();
                int index;
                if (CheckArg(arg, "c") && int.TryParse(args[1], out index))
                {
                    QuickChanger.ChangeWallpaper(index);
                }
            }

            //-- Invalid Arguments -- [Exit]
            else
            {
                MessageBox.Show(
@"Invalid Arguments. Please enter a valid argument:

-c -- Change all wallpapers
-c index -- Change wallpaper for specific screen where index is either 0 or 1
-u -- Update wallpaper for resolution change",
    "Invalid Arguments", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }
        }
    }
}