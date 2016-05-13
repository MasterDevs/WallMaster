using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WallpaperUtils
{
    public class BoundsBuilder
    {
        private bool? _isWindows8;

        public BoundsBuilder(bool? isWindows8OrHigher = null)
        {
            this._isWindows8 = isWindows8OrHigher;
        }

        public bool IsWindows8OrHigher
        {
            get
            {
                if (_isWindows8.HasValue) return _isWindows8.Value;

                if (Environment.OSVersion.Version.Major > 6) return true;
                if (Environment.OSVersion.Version.Major == 6
                     && Environment.OSVersion.Version.Minor >= 2) return true;

                return false;
            }
        }

        public Point GetMinimumBounds(Screen[] screens)
        {
            return IsWindows8OrHigher
                ? GetWin8SMinimumBounds(screens)
                : GetLegacyMinimumBounds(screens);
        }

        public IEnumerable<Rectangle> GetWallpaperBoundsForScreens(Screen[] screens)
        {
            return IsWindows8OrHigher
                ? GetWin8ScreenBounds(screens)
                : GetLegacyScreenBounds(screens);
        }

        private static int MinBounds(Screen[] screens, Func<Screen, int> projection)
        {
            int min = screens.Min(projection);
            min = Math.Min(0, min);

            //min = Math.Abs(min);
            return min;
        }

        private Point GetLegacyMinimumBounds(Screen[] screens)
        {
            Point minimumBounds = new Point(0, 0);
            foreach (var screen in screens)
            {
                minimumBounds.X = Math.Min(screen.Bounds.X, minimumBounds.X);
                minimumBounds.Y = Math.Min(screen.Bounds.Y, minimumBounds.Y);
            }

            return minimumBounds;
        }

        private IEnumerable<Rectangle> GetLegacyScreenBounds(Screen[] screens)
        {
            return from s in screens
                   select s.Bounds;
        }

        private IEnumerable<Rectangle> GetWin8ScreenBounds(Screen[] screens)
        {
            int minX = MinBounds(screens, s => s.Bounds.X);
            int minY = MinBounds(screens, s => s.Bounds.Y);

            Size offSet = new Size(minX, minY);

            var bounds = from s in screens
                         let location = s.Bounds.Location - offSet
                         select new Rectangle(location, s.Bounds.Size);

            return bounds;
        }

        private Point GetWin8SMinimumBounds(Screen[] screens)
        {
            return new Point(0, 0);
        }
    }
}