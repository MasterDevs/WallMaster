using System;
using System.Collections.Generic;
using System.Drawing;

namespace WallpaperUtils {
	public class PaneledImageBuilder {
		#region Fields
		private Size[] _sizes;
		private Image[] _images;
		private Color[] _colors;
		private WallpaperStretchStyle[] _styles;


		/// <summary>
		/// The Size of the Actual image to be built
		/// </summary>
		private Size _aSize;

		/// <summary>
		/// The Actual Image to be built
		/// </summary>
		private Image _aImage;

		private Graphics _g;
		#endregion

		#region Properties

		public Image[] Images {
			get { return _images; }
		}

		public Size[] Sizes {
			get { return _sizes; }
		}

		public Color[] Colors {
			get { return _colors; }
		}

		public WallpaperStretchStyle[] Styles {
			get { return _styles; }
		}

		#endregion


		#region Public Builder Methods

		public Image BuildImage() {
			computeActualSize();
			createImage();
			setBackgroundColors();
			setImages();

			return _aImage;
		}


		public void SetSizes(Size[] sizes) {
			_sizes = sizes;
		}

		public void SetColors(Color[] colors) {
			_colors = colors;
		}

		public void SetImages(Image[] images) {
			_images = images;
		}

		public void SetStretchStyles(WallpaperStretchStyle[] styles) {
			_styles = styles;
		}

		private void setImages() {
			if (_images == null) { return; }

			int length = MinInt(_images.Length, _sizes.Length);
			Point offset = new Point(0, 0);
			for (int x = 0; x < length; x++) {
				Size s = _sizes[x];
				Image i = _images[x];
				WallpaperStretchStyle ss = _styles[x];

				// If no image is set for this region, then skip it
				if (i != null) {
					setImage(i, s, ss, offset);
				}
				offset.X += s.Width;
			}
		}



		private void setImage(Image i, Size s, WallpaperStretchStyle ss, Point offset) {

			switch (ss) {
				case WallpaperStretchStyle.Stretch:
					setStretchImage(i, ref s, ref offset);
					break;
				case WallpaperStretchStyle.StretchRatio:
					setStretchPreserveRatioImage(i, ref s, ref offset);
					break;
				case WallpaperStretchStyle.Magic:
					setMagicImage(i, ref s, ref offset);
					break;
				case WallpaperStretchStyle.Center:
				default:
					setCenteredImage(i, ref s, ref offset);
					break;
			}

		}


		/// <summary>
		/// Draws the image on the local graphic object.
		/// If the image is smaller than s, it will be centered, otherwise it will 
		/// be warped, preseving aspect ratio
		/// </summary>
		/// <param name="i">The image to draw onto the graphic object</param>
		/// <param name="s">
		/// The maximum bounds of the image, the image will be warped to fit
		/// </param>
		/// <param name="offset">The offset on the graphics object to draw the image</param>
		private void setMagicImage(Image i, ref Size s, ref Point offset) {
			if (i.Size.Width > s.Width || i.Size.Height > s.Height) {
				setStretchPreserveRatioImage(i, ref s, ref offset);
			} else {
				setCenteredImage(i, ref s, ref offset);
			}
		}

		/// <summary>
		/// Draws the image (stretched, maintaing aspect ratio) on the local graphic object
		/// </summary>
		/// <param name="i">The image to draw onto the graphic object</param>
		/// <param name="s">
		/// The maximum bounds of the image, the image will be warped to fit
		/// </param>
		/// <param name="offset">The offset on the graphics object to draw the image</param>
		private void setStretchPreserveRatioImage(Image i, ref Size s, ref Point offset) {
			// The ratio is defined as the screen size to image size
			// So 2:1 means that screen is twice as big as the image.
			// 1:2 means that the image is twice as bit as the screen.
			float wRatio = (float)s.Width / i.Size.Width;
			float hRatio = (float)s.Height/ i.Size.Height;

			float ratio = MinFloat(wRatio, hRatio);

			Size newSize = new Size();
			newSize.Width = (int)(ratio * i.Size.Width);
			newSize.Height = (int)(ratio * i.Size.Height);

			Bitmap bi = new Bitmap(i, newSize);

			// At this point the image is stretched, we just need to display it centered.
			// Luckily we have a method for that...
			setCenteredImage(bi, ref s, ref offset);
			
		}


		/// <summary>
		/// Draws the image (stretched, not maintaing aspect ratio) on the local graphic object
		/// </summary>
		/// <param name="i">The image to draw onto the graphic object</param>
		/// <param name="s">
		/// The maximum bounds of the image, the image will be warped to fit
		/// </param>
		/// <param name="offset">The offset on the graphics object to draw the image</param>
		private void setStretchImage(Image i, ref Size s, ref Point offset) {
			Bitmap bi = new Bitmap(i);
			Rectangle r = new Rectangle(offset, s);
			_g.DrawImage(bi, r);
		}

		/// <summary>
		/// Draws the image centered on the local graphic object
		/// </summary>
		/// <param name="i">The image to draw onto the graphic object</param>
		/// <param name="s">
		/// The maximum bounds of the image.
		/// If the image is larger than this, it will be cropepd to fit.
		/// If it is smaller, it will be centered.
		/// </param>
		/// <param name="offset">The offset on the graphics object to draw the image</param>
		private void setCenteredImage(Image i, ref Size s, ref Point offset) {
			Bitmap bi = new Bitmap(i);

			// The location of the top left corner of the image
			Point loc = new Point();
			loc.X = offset.X + s.Width / 2 - bi.Width / 2;
			loc.Y = offset.Y + s.Height / 2 - bi.Height / 2;
			
			// Don't accidently move the picture to the previous screen
			loc.X = MaxInt(loc.X, offset.X);
			loc.Y = MaxInt(loc.Y, offset.Y);

			// size on screen
			Size sos = new Size();
			sos.Width = MinInt(bi.Size.Width, s.Width);
			sos.Height = MinInt(bi.Size.Height, s.Height);



			Rectangle r = new Rectangle(loc, sos);
			_g.DrawImageUnscaledAndClipped(bi, r);
		}


		#endregion

		private void createImage() {
			_aImage = new Bitmap(_aSize.Width, _aSize.Height);
			_g = Graphics.FromImage(_aImage);
		}

		private void setBackgroundColors() {
			if (_colors == null) { return; }

			int length = MinInt(_colors.Length, _sizes.Length);
			Point loc = new Point(0, 0);
			for (int i = 0; i < length; i++) {
				Size s = _sizes[i];
				Color c = _colors[i];

				// If no color is set for this region, then skip it
				if (c == null) { continue; }

				setBackgroundColor(c, loc, s);
				loc.X += s.Width;
			}
		}

		private void setBackgroundColor(Color color, Point loc, Size size) {
			Brush b = new SolidBrush(color);
			Rectangle r = new Rectangle(loc, size);

			_g.FillRectangle(b, r);
		}



		#region Helpers

		private void computeActualSize() {
			_aSize = new Size();
			_aSize.Height = computeHeight();
			_aSize.Width = computeWidth();
		}

		/// <summary>
		/// Computes the width of the final image
		/// </summary>
		/// <remarks>
		/// Width is the summation of all the widths in _sizes.
		/// If _sizes is null, returns 0
		/// </remarks>
		/// <returns>The width of the final image</returns>
		private int computeWidth() {
			if (_sizes == null) { throw new ArgumentException("Sizes has not been set"); }

			int w = 0;
			foreach (Size s in _sizes) {
				w += s.Width;
			}

			return w;
		}

		/// <summary>
		/// Computes the height of the final image
		/// </summary>
		/// <remarks>
		/// Height is the maximum of all the heights in _sizes.
		/// If _sizes is null, returns 0
		/// </remarks>
		/// <returns></returns>
		private int computeHeight() {
			if (_sizes == null) { throw new ArgumentException("Sizes has not been set"); }

			return MaxInt(_sizes.Heights());
		}


		/// <summary>
		/// Returns the minimum of the two floats
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>The minimum value</returns>
		private float MinFloat(float a, float b) {
			if (a < b) { return a; }
			return b;
		}

		/// <summary>
		/// Returns the minimum of the two integers
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>The minimum value</returns>
		private int MinInt(int a, int b) {
			if (a < b) { return a; }
			return b;
		}

		private int MaxInt(int a, int b) {
			if (a > b) { return a; }
			return b;
		}

		/// <summary>
		/// Iterates over the set of integers to determine the largest of them
		/// </summary>
		/// <param name="ints">A set of integers</param>
		/// <returns>The largest integer in the set</returns>
		private int MaxInt(IEnumerable<int> ints) {
			int max = int.MinValue;
			foreach (int i in ints) {
				if (i > max) { max = i; }
			}
			return max;
		}
		#endregion
	}
}