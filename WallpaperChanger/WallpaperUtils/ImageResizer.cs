using System;
using System.Drawing;

namespace WallpaperUtils {
	public class ImageResizer {

		public static Rectangle ResizeImage(Size image, Rectangle bounds, WallpaperStretchStyle style) {
			switch (style) {
				case WallpaperStretchStyle.Center:
					return Center(image, bounds);
				case WallpaperStretchStyle.CenterFit:
					return CenterFit(image, bounds);
				case WallpaperStretchStyle.Stretch:
					return Stretch(image, bounds);
				case WallpaperStretchStyle.Fit:
					return Fit(image, bounds);
				case WallpaperStretchStyle.Fill:
				default:
					return Fill(image, bounds);
			}
		}

		/// <summary>
		/// Image is centered unless it is larger then the desktop.
		/// If it's larger then the desktop, it will be fit,
		/// perserving it's aspect ratio.
		/// </summary>
		/// <param name="image">Size of the image</param>
		/// <param name="bounds">Destination Location and Size</param>
		private static Rectangle CenterFit(Size image, Rectangle bounds) {
			if (image.Height <= bounds.Height && image.Width <= bounds.Width)
				return Center(image, bounds);
			else
				return Fit(image, bounds);
		}

		/// <summary>
		/// If the image is smaller than the bounds, it will be stretched
		/// larger then the bounds, but it's ratio will be preserved and it's
		/// location will be centered on the bounds.
		/// <para>If the image is larger then the bounds, it will be centered
		/// but not resized. This way it's ratio will be preserved, yet
		/// the bounds will be filled.</para>
		/// </summary>
		/// <param name="image">Size of the image</param>
		/// <param name="bounds">Destination Location and Size</param>
		private static Rectangle Fill(Size image, Rectangle bounds) {
			//-- Grab the ratios [Screen size to bounds size]
			float wRatio = (float)bounds.Width / (float)image.Width;
			float hRatio = (float)bounds.Height / (float)image.Height;

			//-- Grab the larger of the two ratios so that
			// the entire bounds will be filled. If we were to get
			// the min of the two ratios, there could be areas
			// of bounds that are not covered by the image.
			float ratio = Math.Max(wRatio, hRatio);

			//-- Create the size of the new Rectangle
			Size size = new Size()
			{
				Width = (int)(ratio * image.Width),
				Height = (int)(ratio * image.Height)
			};

			//-- Center the Rectangle before returning
			return Center(size, bounds);
		}

		/// <summary>
		/// Returns a rectangle that represents the image stretched while
		/// maintaing aspect ratio
		/// </summary>
		private static Rectangle Fit(Size image, Rectangle bounds) {
			//-- Grab the ratios [Screen size to bounds size].
			float wRatio = (float)bounds.Width / image.Width;
			float hRatio = (float)bounds.Height / image.Height;

			//-- Grab the smaller of the to ratios. This will ensure
			// that the image will be scaled, perserving the ratio, as
			// large as it can with out exceeding the bounds size.
			float ratio = Math.Min(wRatio, hRatio);

			//-- Create the size of the new Rectangle
			Size size = new Size()
			{
				Width = (int)(ratio * image.Width),
				Height = (int)(ratio * image.Height)
			};
			//-- Center the Rectangle before returning
			return Center(size, bounds);
		}

		/// <summary>
		/// Returns the bounds which essentially stretches the object.
		/// <para>Note: Stretch does NOT preserve the image ratio</para>
		/// </summary>
		/// <param name="image">Size of the image</param>
		/// <param name="bounds">Bounds of the destination screen</param>
		private static Rectangle Stretch(Size image, Rectangle bounds) {
			return bounds;
		}

		/// <summary>
		/// Returns the bounds of a new image if it were to be centered on the target
		/// </summary>
		/// <param name="image">Size of the image</param>
		/// <param name="bounds">Location & Size of target</param>
		private static Rectangle Center(Size image, Rectangle bounds) {

			int centerBoundsX = (bounds.Width / 2) + bounds.X;
			int centerBoundsY = (bounds.Height / 2) + bounds.Y;
			int centerImageX = (image.Width / 2);
			int centerImageY = (image.Height / 2);

			int x = centerBoundsX - centerImageX;
			int y = centerBoundsY - centerImageY;

			return new Rectangle(x, y, image.Width, image.Height);
		}
	}
}
