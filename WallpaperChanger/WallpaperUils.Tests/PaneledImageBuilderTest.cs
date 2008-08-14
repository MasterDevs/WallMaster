using System;
using System.Drawing;
using NUnit.Framework;
using WallpaperUtils;

namespace WallpaperUils.Tests
{
    [TestFixture]
    public class PaneledImageBuilderTest
    {

        [Test]
        public void SizeTest()
        {
            Size s1 = new Size(10, 1);
            Size s2 = new Size(20, 2);
            Size s3 = new Size(30, 3);
            Size s4 = new Size(40, 4);

            // The size is:
            //      Width = Summation of all the widths
            //      Height = Maximum of all of the heights 
            Size expectedSize = new Size(100, 4);

            PaneledImageBuilder ib = new PaneledImageBuilder();

            ib.SetSizes(new Size[] { s1, s2, s3, s4 });
            Image i = ib.BuildImage();

            Assert.AreEqual(expectedSize, i.Size);
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullSizeTest()
        {
            Size expectedSize = new Size(0, 0);

            PaneledImageBuilder ib = new PaneledImageBuilder();

            ib.SetSizes(null);
            Image i = ib.BuildImage();

            Assert.AreEqual(expectedSize, i.Size);
        }
    }
}
