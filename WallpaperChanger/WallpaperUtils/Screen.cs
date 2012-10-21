using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WallpaperUtils
{
    public class Screen
    {
        public Screen(bool isPrimary, Rectangle bounds)
        {
            this.Primary = isPrimary;
            this.Bounds = bounds;
        }

        public Rectangle Bounds { get; private set; }

        public bool Primary { get; private set; }

        private int Index { get; set; }

        #region Static Methods
        public static int AllScreenCount { get { return AllScreens.Length; } }

        public static Screen[] AllScreens
        {
            get
            {
                //return DoubleScreens();
                //return HandRollScreens();

                return GetActualScreens();
            }
        }

        #region Screen Factories

        private static Rectangle CreateOffsetRectangle(System.Windows.Forms.Screen s)
        {
            int x = s.Bounds.X;
            int y = s.Bounds.Y;

            int w = s.Bounds.Width;
            int h = s.Bounds.Height;

            return new Rectangle(x, y + h, w, h);
        }

        /// <summary>
        /// Mock method used for testing.  This method will return twice the
        /// number of screens you have installed by returning the normal screens
        /// as well as a secondary set of screens based on them, just shifted
        /// down by the height of the corresponding screen.
        /// </summary>
        /// <returns> Twice the number of screens you have installed by returning the normal screens
        /// as well as a secondary set of screens based on them, just shifted
        /// down by the height of the corresponding screen.</returns>
        private static Screen[] DoubleScreens()
        {
            var screens = from s in System.Windows.Forms.Screen.AllScreens
                          select new Screen(s.Primary, s.Bounds);

            var screens2 = from s in System.Windows.Forms.Screen.AllScreens
                           select new Screen(false, CreateOffsetRectangle(s));
            var allScreens = screens.Concat(screens2).OrderBy(s => s.Index).ToArray();
            return allScreens;
        }

        /// <summary>
        /// The production method of getting the screens.
        /// </summary>
        /// <returns>An array of the installed screens currently on the user's machine</returns>
        private static Screen[] GetActualScreens()
        {
            var screens = from s in System.Windows.Forms.Screen.AllScreens
                          select new Screen(s.Primary, s.Bounds);

            return screens.ToArray();
        }

        /// <summary>
        /// Another mock implementation used in testing.  This method will return a hard coded set of screens.  
        /// </summary>
        /// <returns>A hard coded set of screens.  </returns>
        private static Screen[] HandRollScreens()
        {
            /*
             * 10 screens set up in a 2x5 matrix.
             * They all have the same size.
             * 
             * Note the screens may be in an odd order.
             */
            //var r1 = new Rectangle(0, 0, 512, 512);
            //var r2 = new Rectangle(512, 0, 512, 512);
            //var r3 = new Rectangle(1024, 0, 512, 512);
            //var r4 = new Rectangle(1536, 0, 512, 512);
            //var r5 = new Rectangle(2048, 0, 512, 512);
            //var r6 = new Rectangle(0, 512, 512, 512);
            //var r7 = new Rectangle(512, 512, 512, 512);
            //var r8 = new Rectangle(1024, 512, 512, 512);
            //var r9 = new Rectangle(1536, 512, 512, 512);
            //var r10 = new Rectangle(2048, 512, 512, 512);

            //var s1 = new SystemScreen(true, r1) { Index = 2 };
            //var s2 = new SystemScreen(false, r2) { Index = 1 };
            //var s3 = new SystemScreen(false, r3) { Index = 4 };
            //var s4 = new SystemScreen(false, r4) { Index = 3 };
            //var s5 = new SystemScreen(false, r5) { Index = 5 };

            //var s6 = new SystemScreen(false, r6) { Index = 7 };
            //var s7 = new SystemScreen(false, r7) { Index = 5 };
            //var s8 = new SystemScreen(false, r8) { Index = 9 };
            //var s9 = new SystemScreen(false, r9) { Index = 8 };
            //var s10 = new SystemScreen(false, r10) { Index = 10 };

            /*
             * 10 screens set up in a 2x5 matrix.
             * They all have different sizes.
             * 
             * Note the screens may be in an odd order.
             */
            var r1 = new Rectangle(0, 0, 250, 500);
            var r2 = new Rectangle(250, 0, 600, 512);
            var r3 = new Rectangle(850, 0, 250, 524);
            var r4 = new Rectangle(1100, 0, 60, 300);
            var r5 = new Rectangle(1160, 0, 700, 600);
            var r6 = new Rectangle(0, 500, 800, 500);
            var r7 = new Rectangle(800, 512, 900, 780);
            var r8 = new Rectangle(1700, 524, 500, 400);
            var r9 = new Rectangle(2200, 300, 600, 500);
            var r10 = new Rectangle(2800, 600, 460, 504);

            //var s1 = new SystemScreen(true, r1) { Index = 1 };
            //var s2 = new SystemScreen(false, r2) { Index = 2 };
            //var s3 = new SystemScreen(false, r3) { Index = 3 };
            //var s4 = new SystemScreen(false, r4) { Index = 4 };
            //var s5 = new SystemScreen(false, r5) { Index = 5 };
            //var s6 = new SystemScreen(false, r6) { Index = 6 };
            //var s7 = new SystemScreen(false, r7) { Index = 7 };
            //var s8 = new SystemScreen(false, r8) { Index = 8 };
            //var s9 = new SystemScreen(false, r9) { Index = 9 };
            //var s10 = new SystemScreen(false, r10) { Index = 10 };

            var s1 = new Screen(true, r1) { Index = 2 };
            var s2 = new Screen(false, r2) { Index = 1 };
            var s3 = new Screen(false, r3) { Index = 4 };
            var s4 = new Screen(false, r4) { Index = 3 };
            var s5 = new Screen(false, r5) { Index = 5 };
            var s6 = new Screen(false, r6) { Index = 7 };
            var s7 = new Screen(false, r7) { Index = 6 };
            var s8 = new Screen(false, r8) { Index = 9 };
            var s9 = new Screen(false, r9) { Index = 8 };
            var s10 = new Screen(false, r10) { Index = 10 };

            var allScreens = (new List<Screen>() { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 })
                .OrderBy(s => s.Index).ToArray();

            return allScreens;
        }

        #endregion
        #endregion
    }
}