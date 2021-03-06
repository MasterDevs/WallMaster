﻿using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace WallpaperUtils
{
    /// <summary>
    /// The configuration class for a collection of screens
    /// </summary>
    public class WallpaperConfigCollection : List<WallpaperConfig>
    {
        public WallpaperConfigCollection() { }

        public WallpaperConfigCollection(IEnumerable<WallpaperConfig> col)
            : base(col) { }

        public bool ContainsRandom
        {
            get
            {
                foreach (WallpaperConfig wc in this)
                {
                    if (wc.IsRandom) return true;
                }

                return false;
            }
        }

        public static WallpaperConfigCollection GetDefault(int screenCount)
        {
            WallpaperConfigCollection configs = new WallpaperConfigCollection();
            for (int i = 0; i < screenCount; i++)
            {
                configs.Add(WallpaperConfig.GetDefault(i));
            }
            return configs;
        }

        public void ChangeRandomImage()
        {
            foreach (WallpaperConfig wc in this)
            {
                wc.ChangeRandomImage();
            }
        }

        /// <summary>
        /// Returns an array of colors corresponding with this configuration
        /// </summary>
        /// <returns>
        /// An array of colors objects corresponding with this configuration
        /// </returns>
        public Color[] GetColors()
        {
            Color[] colors = new Color[this.Count];

            int x = 0;
            foreach (WallpaperConfig wc in this)
            {
                colors[x++] = wc.BackgroundColor;
            }

            return colors;
        }

        /// <summary>
        /// Returns an array of Image objects corresponding with this configuration
        /// </summary>
        /// <remarks>An item in the array can be null</remarks>
        /// <returns>
        /// An array of Image objects corresponding with this configuration
        /// </returns>
        public Image[] GetImages()
        {
            Image[] images = new Image[this.Count];

            int x = 0;
            foreach (WallpaperConfig wc in this)
            {
                images[x++] = wc.GetImage();
            }

            return images;
        }

        /// <summary>
        /// Returns an array of all screen indexes contained in this collection
        /// </summary>
        public int[] GetScreenIndexes()
        {
            int[] indexes = new int[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                indexes[i] = this[i].ScreenIndex;
            }
            return indexes;
        }

        public WallpaperStretchStyle[] GetStretchStyles()
        {
            WallpaperStretchStyle[] ss = new WallpaperStretchStyle[this.Count];

            int x = 0;
            foreach (WallpaperConfig wc in this)
            {
                ss[x++] = wc.StretchStyle;
            }
            return ss;
        }
    }
}