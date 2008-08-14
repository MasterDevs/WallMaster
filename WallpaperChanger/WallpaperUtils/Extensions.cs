using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WallpaperUtils
{
    public static class Extensions
    {


        public static Size[] ToSizes(this Screen[] s)
        {
            int length = s.Length;

            Size[] sz = new Size[length];
            for (int i = 0; i < length; i++)
            {
                sz[i] = s[i].ToSize();
            }

            return sz;
        }

        public static Size ToSize(this Screen s)
        {
            return s.Bounds.Size;
        }

        public static IEnumerable<int> Heights(this Size[] sizes)
        {
            foreach (Size s in sizes)
            {
                yield return s.Height;
            }
        }


    }
}
