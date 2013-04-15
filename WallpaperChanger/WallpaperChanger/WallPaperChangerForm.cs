﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WallpaperUtils;

namespace WallpaperChanger
{
    public partial class WallpaperChangerForm : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EventHandler ChangeWallpaperEventHandler;

        private WallpaperConfigCollection Configurations;

        private WallpaperCreator _creator;

        private EventHandler DisplaySettingsChangedEventHandler;

        public WallpaperChangerForm()
            : this(null, null, null, null, null)
        {
        }

        public WallpaperChangerForm(WallpaperConfigChanger wallpaperConfigChanger, QuickChanger quickChanger, WallpaperConfigManager configManager, WallpaperCreator creator, WallpaperManager manager)
        {
            InitializeComponent();

            _wallpaperConfigChanger = wallpaperConfigChanger;
            _quickChanger = quickChanger;
            _configManager = configManager;
            _creator = creator;
            _manager = manager;

            LoadConfiguration();
            IntializeEventHandlers();
            _NotifyIcon.Icon = Icon;
            CurrentIndex = 0;

            //-- Load Primary Monitor
            WallpaperPicker_ConfigChanged(null, new ConfigChangedEventArgs(Configurations[CurrentIndex]));
            UserHasMadeAChange = false;
        }

        protected ProcessStartInfo DisplayProperties
        {
            get
            {
                return
                new ProcessStartInfo()
                {
                    FileName = "control.exe",
                    Arguments = "desk.cpl,,3"
                };
            }
        }

        private readonly WallpaperConfigChanger _wallpaperConfigChanger;
        private readonly QuickChanger _quickChanger;
        private readonly WallpaperConfigManager _configManager;
        private readonly WallpaperManager _manager;

        #region Initialization

        private void InitScreens()
        {
            for (int i = 0; i < Configurations.Count; i++)
            {
                _creator.InitScreen(Configurations[i]);
            }
        }

        private void IntializeEventHandlers()
        {
            _WallpaperPicker.ConfigChanged +=
                new EventHandler<ConfigChangedEventArgs>(WallpaperPicker_ConfigChanged);
            DisplaySettingsChangedEventHandler = new EventHandler(DisplaySettingsChanged);
            ChangeWallpaperEventHandler = new EventHandler(ChangeWallpaper);
            SystemEvents.DisplaySettingsChanged += DisplaySettingsChangedEventHandler;
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        private void LoadConfiguration()
        {
            _WallpaperPicker.RaiseEvents = false;
            Configurations = _configManager.Load();
            if (Configurations == null)
            {
                Configurations = WallpaperConfigCollection.GetDefault(WallpaperUtils.Screen.AllScreenCount);
            }

            InitScreens();
            _WallpaperPicker.Config = Configurations[CurrentIndex];
            _WallpaperPicker.RaiseEvents = true;
        }

        private void RefreshConfiguration()
        {
            //-- Load the new configuration
            LoadConfiguration();

            //-- Make sure we highlight the same box
            _creator.SelectedIndex = CurrentIndex;

            //-- Manually refresh the picture box
            ResetPreviewImage();
        }

        #endregion Initialization

        #region Events

        private void AddIndexes(int index)
        {
            for (int i = Configurations.Count; i <= index; i++)
            {
                Configurations.Add(WallpaperConfig.GetDefault(i));
            }
        }

        /// <summary>
        /// We need to detach the DisplaySettingsChangedEventHandler because it's
        /// attached to a static event. Without this, our application can
        /// generate memory leaks.
        /// </summary>
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            SystemEvents.DisplaySettingsChanged -= DisplaySettingsChangedEventHandler;
        }

        private void DisplaySettingsChanged(object sender, EventArgs e)
        {
            InitScreens();
            ResetPreviewImage();
            SaveAndSetWallpaper();
        }

        private void PreviewBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = _creator.GetIndexFromPointOnPreviewImage(_PreviewImageBox.MousePositionOnImage);
            if (index != -1)
            {
                //-- Save current config to configurations
                Configurations[_creator.SelectedIndex] = _WallpaperPicker.Config;

                //-- Set new selected index for creator
                _creator.SelectedIndex = index;

                //-- Pause raising config changed event
                _WallpaperPicker.RaiseEvents = false;

                //-- Initialize WallpaperConfig to be the new configuration
                if (Configurations.Count - 1 < index) //-- If we don't have a config large enough, add them
                    AddIndexes(index);
                _WallpaperPicker.Config = Configurations[index];

                //-- Get the preview image to display the border around the newly selected image
                ResetPreviewImage();

                //-- Resume raising config changed event
                _WallpaperPicker.RaiseEvents = true;

                //-- Change current index
                CurrentIndex = index;
            }
        }

        private void ResetPreviewImage()
        {
            _PreviewImageBox.Image = _creator.PreviewBitmap;
            GC.Collect(); //-- No need to have images in memory if they're not being used
        }

        private void WallpaperPicker_ConfigChanged(object sender, ConfigChangedEventArgs e)
        {
            int idx = _creator.SelectedIndex;
            if (idx != -1)
            {
                e.Config.ScreenIndex = idx;
                _creator.InitScreen(e.Config);
                ResetPreviewImage();
            }

            //-- If we are changing configurations because we are selecting a different
            // wallpaper, then the user has not made a change.
            if (idx == CurrentIndex)
                UserHasMadeAChange = true;

            //-- Reset current index
            CurrentIndex = idx;
        }

        #region Form Events

        /// <summary>
        /// When the form is closed, we only want to hide it, unless it was closed
        /// because of an application exit call or a windows shutdown
        /// </summary>
        private void WallpaperChangerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall &&
                e.CloseReason != CloseReason.WindowsShutDown)
            {
                logger.Debug("Hiding wallpaper changer form");
                Hide();
                e.Cancel = true;
            }

            logger.Info("Closing wallpaper changer form");
        }

        /// <summary>
        /// This method will Start / Stop the Changer whether the form
        /// is not visible / visible respectively.
        /// </summary>
        private void WallpaperChangerForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _wallpaperConfigChanger.Stop();
                RefreshConfiguration();
            }
            else
            {
                _wallpaperConfigChanger.Start();
            }
        }

        #endregion Form Events

        #region Button Events

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            logger.Info("Apply button clicked");
            SaveAndSetWallpaper();
            UserHasMadeAChange = false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            logger.Info("Cancel button clicked");
            LoadConfiguration();
            UserHasMadeAChange = false;
            Hide();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            logger.Info("OK button clicked");
            if (UserHasMadeAChange)
            {
                logger.Info("Saving and applying user changes");
                SaveAndSetWallpaper();
                UserHasMadeAChange = false;
            }
            Hide();
        }

        private void OpenDisplayProperties_Click(object sender, EventArgs e)
        {
            logger.Info("Open display properties menu item clicked");
            Process.Start(DisplayProperties);
        }

        #endregion Button Events

        #region Notify Icon & Context Menu Events

        private void _MMS_File_DropDownOpening(object sender, EventArgs e)
        {
            PopulateMenuItems(_FMI_ChangeWallpaper, _FMI_CW_ChangeAllWallpapers);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog(this);
        }

        /// <summary>
        /// Changes one or all wallpapers, depending on
        /// the tag of the ToolStripMenu item sender
        /// </summary>
        private void ChangeWallpaper(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            int index = (int)tsmi.Tag;
            if (index == -1)
                _quickChanger.ChangeAllWallpapers();
            else
                _quickChanger.ChangeWallpaper(index);

            RefreshConfiguration();
        }

        /// <summary>
        /// This method is necessary to dynamically populate
        /// the change screen menu items. It'll allow the use to change
        /// 1 or all of the screens if their are multiple screens, or just
        /// one screen if there is just one.
        /// </summary>
        private void ChangeWallpaperMenuStrip_Opened(object sender, CancelEventArgs e)
        {
            PopulateMenuItems(_CMI_ChangeWallpaper, _CMI_CW_ChangeAllWallpapers);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            logger.Info("Exit menu item selected.  Closing the application.");
            Application.Exit();
        }

        private void PopulateMenuItems(ToolStripMenuItem changeWallpaper, ToolStripMenuItem changeAll)
        {
            changeWallpaper.DropDownItems.Clear();
            changeWallpaper.Click -= ChangeWallpaperEventHandler;

            //-- If we only have 1 monitor, we don't need to have any drop downs
            if (WallpaperUtils.Screen.AllScreenCount == 1)
            {
                changeWallpaper.Click += ChangeWallpaperEventHandler;
                changeWallpaper.Tag = 0;
            }
            else
            { //-- More than 1 monitor
                changeAll.Tag = -1;
                changeWallpaper.DropDownItems.Add(changeAll);

                changeWallpaper.DropDownItems.Add(new ToolStripSeparator());
                for (int i = 0; i < WallpaperUtils.Screen.AllScreenCount; i++)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(string.Format("Change Screen {0}", (i + 1)));
                    tsmi.Click += ChangeWallpaperEventHandler;
                    tsmi.Tag = i;
                    changeWallpaper.DropDownItems.Add(tsmi);
                }
            }
        }

        private void ShowWallMaster(object sender, EventArgs e)
        {
            logger.Info("Showing form after it was hidden.");
            Show();
            Activate();
        }

        private void UpdateWallpaper_Click(object sender, EventArgs e)
        {
            logger.Info("Update menu item selected.");
            _quickChanger.UpdateWallpaperForResolutionChange();
        }

        #endregion Notify Icon & Context Menu Events

        #endregion Events

        #region Helper Methods

        private int CurrentIndex { get; set; }

        /// <summary>
        /// This property will be used to denote whether or not a user has made a change.
        /// </summary>
        private bool UserHasMadeAChange
        {
            get { return _ApplyButton.Enabled; }
            set { _ApplyButton.Enabled = value; }
        }

        private void SaveAndSetWallpaper()
        {
            _configManager.Save(Configurations);
            string path = _configManager.WallpaperPath;
            _creator.DesktopBitmap.Save(path, ImageFormat.Png);

            _manager.SetWallpaper(path);
            GC.Collect(); //-- Force another collection
        }

        #endregion Helper Methods

        private void openWallMasterDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = _configManager.AppDir;
            try
            {
                Process.Start(dir);
            }
            catch (Exception ex)
            {
                dir = string.IsNullOrEmpty(dir) ? "<no directory specified>" : dir;
                string msg = "Could not open WallMaster directory:  {0}" + dir;

                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(msg, ex);
            }
        }
    }
}