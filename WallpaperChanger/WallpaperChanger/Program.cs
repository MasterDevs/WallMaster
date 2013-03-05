﻿using log4net;
using System;
using System.IO;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger
{
    internal static class Program
    {
        private const string HELP_TEXT =
@"Invalid Arguments. Please enter a valid argument:

-c -- Change all wallpapers
-c index -- Change wallpaper for specific screen where index is either 0 or 1
-u -- Update wallpaper for resolution change";

        private static log4net.ILog _logger;

        private static string LogFilePath
        {
            get
            {
                string path = Path.Combine(WallpaperConfigManager.AppDir, @"WallMaster.log");
                return path;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            SetUpLogger();

            _logger.Info("Application started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ParseArgs(args);
            _logger.Info("Application closed");
        }

        /// <summary>
        /// This method will check for all argument variations of a desired argument
        /// </summary>
        /// <param name="desiredArg">Just the character, no flags like '/' or '-' or '--'</param>
        /// <returns>True if inputted argument is the desired argument</returns>
        private static bool CheckArg(string inputedArg, string desiredArg)
        {
            return
                inputedArg == desiredArg ||
                inputedArg == string.Format("-{0}", desiredArg) ||
                inputedArg == string.Format("--{0}", desiredArg) ||
                inputedArg == string.Format("/{0}", desiredArg) ||
                inputedArg == string.Format(@"\{0}", desiredArg);
        }

        private static void HandleComplexArgumens(string[] args)
        {
            string arg = args[0].ToLower();
            string screenIndex = args[1];

            _logger.InfoFormat("Started with the following arguments:  [{0}], [{1}]", arg, screenIndex);

            int index;
            if (CheckArg(arg, "c") && int.TryParse(screenIndex, out index))
            {
                QuickChanger.ChangeWallpaper(index);
            }
            else
            {
                _logger.Fatal("Failed to handle arguments");
            }
        }

        private static void HandleInvalidArguments()
        {
            MessageBox.Show(HELP_TEXT,
                "Invalid Arguments",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);

            _logger.Fatal(HELP_TEXT);
            _logger.Fatal("Application closing because of invalid arguments");

            Application.Exit();
        }

        private static void HandleNoArguments()
        {
            _logger.Info("No arguments provided, opening wallpaper configuration form");
            Application.Run(new WallpaperChangerForm());
        }

        private static void HandleSimpleArguments(string[] args)
        {
            string arg = args[0].ToLower();
            _logger.Info("Started with the following arguments:  " + arg);

            if (CheckArg(arg, "c"))
            {
                QuickChanger.ChangeAllWallpapers();
            }
            else if (CheckArg(arg, "u"))
            {
                QuickChanger.UpdateWallpaperForResolutionChange();
            }
            else if (CheckArg(arg, "h"))
            {
                //-- Hidden Mode - Start Minimized to Tray
                new WallpaperChangerForm();

                WallpaperConfigChanger.Start(); // If the user has random wallpapers, we need to begin changing
                Application.Run();
            }
        }

        private static void ParseArgs(string[] args)
        {
            switch (args.Length)
            {
                case 0: HandleNoArguments(); return;
                case 1: HandleSimpleArguments(args); return;
                case 2: HandleComplexArgumens(args); return;
                default: HandleInvalidArguments(); return;
            }
        }

        private static void SetUpLogger()
        {
            try
            {
                GlobalContext.Properties["LogName"] = LogFilePath;
                log4net.Config.XmlConfigurator.Configure();

                _logger = LogManager.GetLogger(typeof(Program));
            }
            catch
            {
                // if we can't create the logger then there's nothing we can really do here...
            }
        }
    }
}