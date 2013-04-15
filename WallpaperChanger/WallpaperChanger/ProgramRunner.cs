﻿using log4net;
using System.IO;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger
{
    public class ProgramRunner
    {
        public ProgramRunner(WallpaperChangerForm form, WallpaperConfigChanger wallpaperConfigChanger, QuickChanger quickChanger, WallpaperConfigManager configManager)
        {
            _form = form;
            _wallpaperConfigChanger = wallpaperConfigChanger;
            _quickChanger = quickChanger;
            _configManager = configManager;
        }

        public void Run(string[] args)
        {
            SetUpLogger();

            _logger.Info("Application started");
            ParseArgs(args);
            _logger.Info("Application closed");
        }

        private const string HELP_TEXT =
@"Invalid Arguments. Please enter a valid argument:

-c -- Change all wallpapers
-c index -- Change wallpaper for specific screen where index is either 0 or 1
-u -- Update wallpaper for resolution change";

        private log4net.ILog _logger;
        private readonly WallpaperConfigChanger _wallpaperConfigChanger;
        private readonly WallpaperChangerForm _form;
        private readonly QuickChanger _quickChanger;
        private readonly WallpaperConfigManager _configManager;

        private string LogFilePath
        {
            get
            {
                string path = Path.Combine(_configManager.AppDir, @"WallMaster.log");
                return path;
            }
        }

        /// <summary>
        /// This method will check for all argument variations of a desired argument
        /// </summary>
        /// <param name="desiredArg">Just the character, no flags like '/' or '-' or '--'</param>
        /// <returns>True if inputted argument is the desired argument</returns>
        private bool CheckArg(string inputedArg, string desiredArg)
        {
            return
                inputedArg == desiredArg ||
                inputedArg == string.Format("-{0}", desiredArg) ||
                inputedArg == string.Format("--{0}", desiredArg) ||
                inputedArg == string.Format("/{0}", desiredArg) ||
                inputedArg == string.Format(@"\{0}", desiredArg);
        }

        private void HandleComplexArgumens(string[] args)
        {
            string arg = args[0].ToLower();
            string screenIndex = args[1];

            _logger.InfoFormat("Started with the following arguments:  [{0}], [{1}]", arg, screenIndex);

            int index;
            if (CheckArg(arg, "c") && int.TryParse(screenIndex, out index))
            {
                _quickChanger.ChangeWallpaper(index);
            }
            else
            {
                _logger.Fatal("Failed to handle arguments");
            }
        }

        private void HandleInvalidArguments()
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

        private void HandleNoArguments()
        {
            _logger.Info("No arguments provided, opening wallpaper configuration form");
            Application.Run(_form);
        }

        private void HandleSimpleArguments(string[] args)
        {
            string arg = args[0].ToLower();
            _logger.Info("Started with the following arguments:  " + arg);

            if (CheckArg(arg, "c"))
            {
                _quickChanger.ChangeAllWallpapers();
            }
            else if (CheckArg(arg, "u"))
            {
                _quickChanger.UpdateWallpaperForResolutionChange();
            }
            else if (CheckArg(arg, "h"))
            {
                _wallpaperConfigChanger.Start(); // If the user has random wallpapers, we need to begin changing
                Application.Run();
            }
        }

        private void ParseArgs(string[] args)
        {
            switch (args.Length)
            {
                case 0: HandleNoArguments(); return;
                case 1: HandleSimpleArguments(args); return;
                case 2: HandleComplexArgumens(args); return;
                default: HandleInvalidArguments(); return;
            }
        }

        private void SetUpLogger()
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