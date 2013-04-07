using System.Drawing.Imaging;
using System.Linq;

namespace WallpaperUtils
{
    /// <summary>
    /// This class exists to provide quick access to some routine tasks such as:
    /// <para>Change Wallpaper [Screen 1, 2 or Both]</para>
    /// <para>Adjust for resolution change</para>
    /// </summary>
    public static class QuickChanger
    {
        private static readonly object lockObj;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static WallpaperConfigCollection Configuration;
        private static WallpaperCreator Creator;

        static QuickChanger()
        {
            lockObj = new object();
        }

        private static bool CouldNotLoadConfiguration { get { return Configuration == null; } }

        /// <summary>
        /// Changes all background images for screens that's configuration is set to
        /// random.
        /// </summary>
        public static void ChangeAllWallpapers()
        {
            logger.Debug("Changing all random wallpapers...");

            Configuration = WallpaperConfigManager.Load();
            if (CouldNotLoadConfiguration)
            {
                logger.Error("Could not change any wallpapers:  Could not load configuration");
                return;
            }

            bool has_A_Random_Screen = InitScreens(true);

            if (!has_A_Random_Screen)
            {
                logger.Error("Could not change any wallpapers:  No random wallpapers in configuration");

                return;
            }

            SetWallpaperAndSave();

            logger.Debug("All random wallpapers successfully changed.");
        }

        /// <summary>
        /// Changes the background image for a particular screen.
        /// </summary>
        /// <param name="screenIndex">Screen Index that you would like to change</param>
        /// <returns>True if the image was changed, false otherwise</returns>
        public static void ChangeWallpaper(int screenIndex)
        {
            ChangeWallpaper(new int[1] { screenIndex });
        }

        /// <summary>
        /// Changes the wallpaper for an array of screen indexes.
        /// <para>Note: If there is any config that is not random, the wallpaper will not be changed.</para>
        /// </summary>
        public static void ChangeWallpaper(int[] screenIndexes)
        {
            if (screenIndexes == null || screenIndexes.Length == 0)
            {
                logger.Warn("Cannot change wallpapers:  no screens specified");
            }

            logger.InfoFormat("Changing wallpapers for screens:  {0}",
                string.Join(", ", screenIndexes.Select(i => i.ToString()).ToArray()));

            Configuration = WallpaperConfigManager.Load();

            if (CouldNotLoadConfiguration)
            {
                logger.Warn("Could not change wallpapers:  could not load configuration");
                return;
            }

            foreach (int index in screenIndexes)
            {
                if (InValidScreenIndex(index) || NotARandomConfig(index))
                {
                    logger.Warn("Could not change wallpapers:  index {0} is either invalid or does not support changing");
                    return;
                }
            }

            //-- If we've made it this far, we're ok to change the wallpaper(s)
            foreach (int index in screenIndexes)
            {
                Configuration[index].ChangeRandomImage();
            }

            InitScreens(false);

            SetWallpaperAndSave();

            logger.Debug("Wallpapers successfully changed.");
        }

        /// <summary>
        /// This method will update all screen images to reflect
        /// a change in resolution.
        /// <para>If any screens are set to random, this will NOT change
        /// the screen. Please use ChangeWallpaper or ChangeAllWallpapers for that</para>
        /// </summary>
        public static void UpdateWallpaperForResolutionChange()
        {
            logger.Debug("Updating all screen images to reflect a change in resolution...");

            Configuration = WallpaperConfigManager.Load();

            if (CouldNotLoadConfiguration)
            {
                logger.Warn("Could not update screens:  could not load configuration");
                return;
            }

            if (Screen.AllScreenCount > Configuration.Count)
            {
                logger.Warn("Could not update screens:  There are more screens than we have configurations");
                return;
            }

            InitScreens(0, Screen.AllScreenCount, false);

            SetWallpaperAndSave();
            Creator.Update(false, true);

            logger.Debug("Updated all screen images to reflect a change in resolution successfully.");
        }

        /// <summary>
        /// Helper method ensures that we dispose the Creator object
        /// before we instantiate a new one.
        /// </summary>
        private static void InitCreator()
        {
            Creator = new WallpaperCreator();
        }

        /// <summary>
        /// Iterates all configurations from startIndex to endIndex.
        /// <para>Returns true if there was a random screen, false otherwise</para>
        /// </summary>
        /// <param name="startIndex">Start Index in Configurations</param>
        /// <param name="endIndex">End Index in Configurations</param>
        /// <param name="change">If true and screen is set to random, this will change the image</param>
        /// <returns>True if there was a random screen, false otherwise</returns>
        private static bool InitScreens(int startIndex, int endIndex, bool change)
        {
            InitCreator();

            bool has_A_Random_Screen = false;

            //-- Change all of the wallpapers that are set as random images
            for (int i = startIndex; i < endIndex; i++)
            {
                if (Configuration[i].IsRandom && change)
                {
                    Configuration[i].ChangeRandomImage();
                    has_A_Random_Screen = true;
                }
                Creator.InitScreen(Configuration[i]);
            }
            return has_A_Random_Screen;
        }

        /// <summary>
        /// Refreshes all configurations for current screen
        /// <para>Returns true if there was a random screen, false otherwise</para>
        /// </summary>
        /// <param name="change">True if you want to change any random screen</param>
        /// <returns>True if there was a random screen, false otherwise</returns>
        private static bool InitScreens(bool change)
        {
            return InitScreens(0, Screen.AllScreenCount, change);
        }

        /// <summary>
        /// Checks if the screen index is between 0 and Screen Count - 1
        /// </summary>
        /// <returns>True if screenIndex is valid, false otherwise</returns>
        private static bool InValidScreenIndex(int screenIndex)
        {
            if (screenIndex < 0 ||
                screenIndex > (Configuration.Count - 1) ||
                screenIndex > (Screen.AllScreenCount - 1))
            {
                logger.Warn("Invalid screen index:  " + screenIndex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if screen index is valid and refers to a random configuration
        /// </summary>
        private static bool NotARandomConfig(int screenIndex)
        {
            if (!Configuration[screenIndex].IsRandom)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the wallpaper and saves the current configuration
        /// </summary>
        private static void SetWallpaperAndSave()
        {
            logger.Debug("Setting wallpaper and saving configuration...");
            string path = WallpaperConfigManager.WallpaperPath;
            lock (lockObj)
            {
                Creator.DesktopBitmap.Save(path, ImageFormat.Png);
            }
            WallpaperManager.SetWallpaper(path);
            logger.Debug("Wallpaper set.");

            //-- Save the configuration so we know what the current images are
            WallpaperConfigManager.Save(Configuration);
            logger.Debug("Configuration saved...");
        }
    }
}