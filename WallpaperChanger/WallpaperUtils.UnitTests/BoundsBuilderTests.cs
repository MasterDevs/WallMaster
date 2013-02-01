using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;

namespace WallpaperUtils.UnitTests
{
    [TestClass]
    public class BoundsBuilderTests
    {
        private Size LargeScreenSize = new Size(1920, 1080);
        private Size SmallScreenSize = new Size(1650, 1050);

        [TestMethod]
        public void GetWallpaperBoundsForScreens_PrimaryMonitorOnTheLeft_PositionsImagesCorrectly()
        {
            // Assemble
            var inputPrimaryScreen = new Point(0, 0);
            var inputSecondaryScreen = new Point(0, 1050);

            var expectedPrimaryBound = new Point(0, 0);
            var expectedSecondaryBound = new Point(0, 1050);

            // Act/Assert
            TestWallpaperBoundsForScreens(
                inputPrimaryScreen,
                inputSecondaryScreen,
                expectedPrimaryBound,
                expectedSecondaryBound
                );
        }

        [TestMethod]
        public void GetWallpaperBoundsForScreens_PrimaryMonitorOnTheRight_PositionsImagesCorrectly()
        {
            // Assemble
            var inputPrimaryScreen = new Point(0, 0);
            var inputSecondaryScreen = new Point(-1920, 0);

            var expectedPrimaryBound = new Point(1920, 0);
            var expectedSecondaryBound = new Point(0, 0);

            // Act/Assert
            TestWallpaperBoundsForScreens(
                inputPrimaryScreen,
                inputSecondaryScreen,
                expectedPrimaryBound,
                expectedSecondaryBound
                );
        }

        [TestMethod]
        public void GetWallpaperBoundsForScreens_PrimaryMonitorOnTheRightAndAbove_PositionsImagesCorrectly()
        {
            // Assemble
            var inputPrimaryScreen = new Point(0, 0);
            var inputSecondaryScreen = new Point(-1920, 546);

            var expectedPrimaryBound = new Point(1920, 0);
            var expectedSecondaryBound = new Point(0, 546);

            // Act/Assert
            TestWallpaperBoundsForScreens(
                inputPrimaryScreen,
                inputSecondaryScreen,
                expectedPrimaryBound,
                expectedSecondaryBound
                );
        }

        [TestMethod]
        public void GetWallpaperBoundsForScreens_PrimaryMonitorOnTheRightAndBelow_PositionsImagesCorrectly()
        {
            // Assemble
            var inputPrimaryScreen = new Point(0, 0);
            var inputSecondaryScreen = new Point(-1920, -1080);

            var expectedPrimaryBound = new Point(1920, 1080);
            var expectedSecondaryBound = new Point(0, 0);

            // Act/Assert
            TestWallpaperBoundsForScreens(
                inputPrimaryScreen,
                inputSecondaryScreen,
                expectedPrimaryBound,
                expectedSecondaryBound
                );
        }

        [TestMethod]
        public void GetWallpaperBoundsForScreens_PrimaryMonitorOnTheRightAndBottomAligned_PositionsImagesCorrectly()
        {
            // Assemble
            var inputPrimaryScreen = new Point(0, 0);
            var inputSecondaryScreen = new Point(-1920, -30);

            var expectedPrimaryBound = new Point(1920, 30);
            var expectedSecondaryBound = new Point(0, 0);

            // Act/Assert
            TestWallpaperBoundsForScreens(
                inputPrimaryScreen,
                inputSecondaryScreen,
                expectedPrimaryBound,
                expectedSecondaryBound
                );
        }

        private Screen CreateScreen(Point location, Size size)
        {
            Rectangle r = new Rectangle(location, size);
            return new Screen(false, r);
        }

        private void TestWallpaperBoundsForScreens(
            Point inputPrimaryScreen, Point inputSecondaryScreen,
            Point expectedPrimaryBound, Point expectedSecondaryBound)
        {
            // Assemble
            Screen[] screens = new Screen[]{
                CreateScreen(inputSecondaryScreen, LargeScreenSize),
                CreateScreen(inputPrimaryScreen, SmallScreenSize),
            };

            Rectangle[] expectedBounds = new Rectangle[] {
                                        new Rectangle(expectedSecondaryBound, LargeScreenSize),
                                        new Rectangle(expectedPrimaryBound, SmallScreenSize),
            };
            BoundsBuilder bb = new BoundsBuilder(isWindows8OrHigher: true);

            // Act
            var actualBounds = bb.GetWallpaperBoundsForScreens(screens).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(expectedBounds, actualBounds);
        }
    }
}