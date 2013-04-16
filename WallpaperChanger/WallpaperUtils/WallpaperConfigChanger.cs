using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using SysTimers = System.Timers;

namespace WallpaperUtils
{
    /// <summary>
    /// This class is designed to have two public facing methods - Start & Stop
    /// When started, this class will call QuickChanger, updating the appropriate screens.
    /// If there are multiple wallpapers and events overlap, only one call will
    /// be made to QuickChanger.
    /// </summary>
    public class WallpaperConfigChanger
    {
        #region WallpaperChanged Event

        public event EventHandler<WallpaperChangeEventArgs> WallpaperChanged = delegate { };

        public class WallpaperChangeEventArgs : EventArgs
        {
            public readonly WallpaperSettings Settings;

            public WallpaperChangeEventArgs(WallpaperSettings settings)
            {
                Settings = settings;
            }
        }

        #endregion WallpaperChanged Event

        private readonly ILogger _logger;
        private double Count;
        private double GCD;
        private object LockObj = new object();
        private SysTimers.Timer TheTimer;

        /// <summary>
        /// Dictionary of ScreenIndexes to Interval Counts
        /// </summary>
        private SortedList<double, List<int>> TimeIntervals;

        private readonly QuickChanger _quickChanger;
        private readonly WallpaperConfigManager _configManager;

        public void Start()
        {
            lock (LockObj)
            {
                _logger.Debug("Starting wallpaper changer");

                var settings = _configManager.Load();
                if (settings == null)
                {
                    _logger.Warn("Can't start wallpaper changer:  could not load configuration");
                    return;
                }

                //-- Check if we have a random screen config
                bool noRandomScreenConfig = IsThereNoRandomConfig(settings.ScreenConfigs);

                if (noRandomScreenConfig)
                {
                    _logger.Warn("Can't start wallpaper changer:  No random screen configurations found");
                    return;
                }

                ComputeTimeIntervals(settings.ScreenConfigs);

                CalculateGCD();

                CreateNewTimer(GCD);

                InitializeTimerCount();

                StartTimer();

                _logger.Debug("Wallpaper changer started");
            }
        }

        public void Stop()
        {
            lock (LockObj)
            {
                if (TheTimer != null)
                {
                    StopTimer();
                    TheTimer.Elapsed -= TimerTick;
                    TheTimer.Dispose();
                }
            }
        }

        /// <summary>
        /// This method will grab the GCD for all screen indexes.
        /// </summary>
        private void CalculateGCD()
        {
            //-- If all random configurations are to be changed on same interval,
            // the GCD is the interval.
            if (TimeIntervals.Count == 1)
            {
                GCD = TimeIntervals.Keys[0];
                return;
            }

            GCD = 1;
            IList<double> times = TimeIntervals.Keys;
            for (int i = 0; i < times.Count - 1; i++)
            {
                double gcd_temp = GreatestCommonDenominator(times[i], times[i + 1]);
                if (gcd_temp > GCD && GCDWorksForAllTimes(gcd_temp))
                    GCD = gcd_temp;
            }
        }

        public WallpaperConfigChanger(QuickChanger quickChanger, WallpaperConfigManager configManager, ILogger logger)
        {
            _quickChanger = quickChanger;
            _configManager = configManager;
            _logger = logger;
        }

        private void ChangeWallpaper()
        {
            lock (LockObj)
            {
                try
                {
                    _logger.Debug("Changing wallpaper");
                    StopTimer();
                    List<int> indexesToChange = new List<int>(TimeIntervals.Count);

                    foreach (var time in TimeIntervals.Keys)
                    {
                        if (((Count * GCD) % time) == 0)
                            indexesToChange.AddRange(TimeIntervals[time]);
                    }

                    _quickChanger.ChangeWallpaper(indexesToChange.ToArray());

                    RaiseWallpaperChanged(indexesToChange);

                    Count++;

                    _logger.Debug("Wallpaper changed");
                }
                catch (Exception ex)
                {
                    _logger.Error("There was an error attempting to change the wallpaper", ex);
                }
                finally
                {
                    StartTimer();
                }
            }

            //-- Manually Force Garbage Collection to keep things tidy
            // This is especially useful when the image change interval is 1 minute
            GC.Collect();
        }

        private void ComputeTimeIntervals(WallpaperConfigCollection configs)
        {
            TimeIntervals = new SortedList<double, List<int>>(configs.Count);

            var randomConfigs = from c in configs
                                where c.IsRandom
                                select new
                                {
                                    ScreenIndex = c.ScreenIndex,
                                    ChangeIntervalInSeconds = c.ChangeWallpaperInterval.TotalSeconds
                                };

            foreach (var c in randomConfigs)
            {
                //-- Find out what the interval is, based on total seconds, and add it to the list
                if (TimeIntervals.ContainsKey(c.ChangeIntervalInSeconds))
                    TimeIntervals[c.ChangeIntervalInSeconds].Add(c.ScreenIndex);
                else
                    TimeIntervals[c.ChangeIntervalInSeconds] = new List<int> { c.ScreenIndex };
            }
        }

        private void CreateNewTimer(double timerIntervalSeconds)
        {
            lock (LockObj)
            {
                if (TheTimer != null)
                    TheTimer.Dispose();
                TheTimer = new SysTimers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
                TheTimer.Elapsed += TimerTick;
                TheTimer.Interval = TimeSpan.FromSeconds(timerIntervalSeconds).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Checks to see if the possible GCD works for all time values.
        /// If so, returns true, false otherwise.
        /// </summary>
        private bool GCDWorksForAllTimes(double gcd_temp)
        {
            foreach (double time in TimeIntervals.Keys)
            {
                if (time % gcd_temp != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Simple recursive way to find the GCD.
        /// </summary>
        private double GreatestCommonDenominator(double a, double b)
        {
            if (a == 0)
                return b;
            else if (b == 0)
                return a;

            if (a > b)
                return GreatestCommonDenominator(a % b, b);
            else
                return GreatestCommonDenominator(a, b % a);
        }

        private void InitializeTimerCount()
        {
            _logger.Debug("Initializing the timer counter to 1");
            Count = 1;
        }

        /// <summary>
        /// Returns true if there are no random configurations in the config
        /// </summary>
        /// <param name="configs"></param>
        /// <returns></returns>
        private bool IsThereNoRandomConfig(WallpaperConfigCollection configs)
        {
            foreach (var c in configs)
            {
                if (c.IsRandom && c.ScreenIndex < Screen.AllScreenCount)
                {
                    return false;
                }
            }
            return true;
        }

        private void RaiseWallpaperChanged(List<int> indexesToChange)
        {
            if (indexesToChange.Count > 0)
            {
                //-- Fire event notifying that a wallpaper has been changed
                WallpaperChanged(null, new WallpaperChangeEventArgs(_configManager.Load()));
            }
        }

        private void StartTimer()
        {
            _logger.Debug("Starting the timer");
            TheTimer.Start();
        }

        private void StopTimer()
        {
            _logger.Debug("Stopping the timer");
            TheTimer.Stop();
        }

        private void TimerTick(object sender, SysTimers.ElapsedEventArgs e)
        {
            _logger.Info("Timer tick");
            ChangeWallpaper();
        }
    }
}