using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace WallpaperUtils {
	public class WallpaperCreator : IDisposable {

		private Bitmap previewBitmap = null, desktopBitmap = null;
		private Color DefaultBackgroundColor = Color.Black;
		private WallpaperStretchStyle DefaultStyle = WallpaperStretchStyle.Fill;
		private Pen CaptionOutline = new Pen(Color.Black, 3f);
		private Brush CaptionFill = new SolidBrush(Color.White);
		private Color BorderColorStandard = Color.White;
		private Color BorderColorSelected = Color.Red;
		private float BorderSize = 8;

		/// <summary>
		/// This is the 0,0 location on the composite desktop image.
		/// It's used to tra
		/// </summary>
		private Point refPoint;

		/// <summary>
		/// This group of rectangles is necessary for determining which
		/// screen if any is located at a particular point.
		/// </summary>
		public Rectangle[] previewBounds;

		private Brush[] BackgroundColors;
		private Screen[] Screens { get { return Screen.AllScreens; } }
		private string[] wallpaperFilenames;
		private WallpaperStretchStyle[] Styles;
		private Dictionary<int, WeakReference> wallpaperBitmapCache;
		private int _selectedIndex = -1;
		private string PreviewBitmapChecksum;
		private string DesktopBitmapChecksum;

		// This collection isn't actually used to retrieve the Bitmap, but
		// prevents the WeakReference from disappearing while the Bitmap is in use
		Bitmap[] wallpaperBitmaps;

		public WallpaperCreator() {
			UpdateMonitorBounds();

			wallpaperBitmapCache = new Dictionary<int, WeakReference>();

			// Initialize collections (should really be re-init'd when screens change...)
			wallpaperFilenames = new string[Screens.Length];
			wallpaperBitmaps = new Bitmap[Screens.Length];
			previewBounds = new Rectangle[Screens.Length];
			BackgroundColors = new Brush[Screens.Length];
			Styles = new WallpaperStretchStyle[Screens.Length];

			//-- Initialize Default Background Colors & Styles
			for (int i = 0; i < Screens.Length; i++) {
				BackgroundColors[i] = new SolidBrush(DefaultBackgroundColor);
				Styles[i] = DefaultStyle;
			}
			_selectedIndex = 0;
			PreviewBitmapChecksum = DesktopBitmapChecksum = string.Empty;
		}

		#region Public Properties
		public int SelectedIndex {
			get { return _selectedIndex; }
			set {
				if (value < previewBounds.Length && value > -2) {
					_selectedIndex = value;
				}
			}
		}

		public Screen SelectedScreen {
			get {
				if (SelectedIndex != -1 && SelectedIndex < Screens.Length)
					return Screens[SelectedIndex];
				else
					return null;
			}
		}

		/// <summary>
		/// Returns the Preview Bitmap
		/// <para>Note: The preview bitmap will be updated before it's returned</para>
		/// </summary>
		public Bitmap PreviewBitmap {
			get {
				Update(false, true); //-- No need to update desktop image
				return previewBitmap;
			}
		}

		/// <summary>
		/// Returns the Desktop Bitmap
		/// <para>Note: The desktop bitmap will be updated before it's returned</para>
		/// </summary>
		public Bitmap DesktopBitmap {
			get {
				Update(true, false); //-- No need to update preview image
				return desktopBitmap;
			}
		}
		#endregion

		#region "Desktop updating"

		/// <summary>
		/// Modify the preview window (wrapping images as used for Desktop)
		/// </summary>
		private void UpdateDesktopImage() {
			using (Graphics g = Graphics.FromImage(desktopBitmap)) {
				for (int idx = 0; idx < Screens.Length; idx++) {
					AddImageToDesktop(g, GetResizedBitmap(idx), Screens[idx].Bounds, idx);
				}
			}
		}

		private Bitmap GetResizedBitmap(int idx) {
			Bitmap image = GetBitmap(idx);
			Rectangle bounds = new Rectangle(0, 0, Screens[idx].Bounds.Width, Screens[idx].Bounds.Height);
			Bitmap bm = new Bitmap(bounds.Width, bounds.Height);
			using (Graphics g = Graphics.FromImage(bm)) {
				//-- Fill BackgroundColor
				g.FillRectangle(BackgroundColors[idx], bounds);

				if (image != null) {

					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					g.DrawImage(image, ImageResizer.ResizeImage(image.Size, bounds, Styles[idx]));
				}
			}
			return bm;
		}

		/// <summary>
		/// Adds the given image to the bitmap, but wraps images as needed based on monitor configuration
		/// </summary>
		/// <param name="g">The Graphics of the composite Desktop </param>
		/// <param name="image">Image that we'll be drawing to the graphic</param>
		/// <param name="bounds">The bounds for this screen</param>
		/// <param name="idx">Index to the background color we'll be using</param>
		private void AddImageToDesktop(Graphics g, Bitmap image, Rectangle bounds, int idx) {
			// Draw the background color
			g.FillRectangle(BackgroundColors[idx], bounds);

			//-- If we don't have an image, simply return
			if (image == null)
				return;

			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			Rectangle sourceBounds = new Rectangle(0, 0, image.Width, image.Height);
			bounds = ImageResizer.ResizeImage(image.Size, bounds, Styles[idx]);

			// If image is drawn outside of viewable area, it must be "wrapped" around
			if (bounds.X < 0 || bounds.Y < 0 ||
					(bounds.X + bounds.Width > desktopBitmap.Width) || (bounds.Y + bounds.Height > desktopBitmap.Height)) {
				// Draw the first half
				int x = bounds.X, y = bounds.Y;

				if (bounds.Y < 0)
					y = desktopBitmap.Height + bounds.Y;
				else if (bounds.Y + bounds.Height > desktopBitmap.Height)
					y = 0 - (desktopBitmap.Height - bounds.Height + bounds.Y);

				if (bounds.X < 0)
					x = desktopBitmap.Width + bounds.X;
				else if (bounds.X + bounds.Width > desktopBitmap.Width)
					x = 0 - (desktopBitmap.Width - bounds.Width - bounds.X);

				// Draw original coordinates
				g.DrawImage(image, new Rectangle(x, y, bounds.Width, bounds.Height),
					new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

				// Draw with corrected Y coordinate
				g.DrawImage(image, new Rectangle(x, bounds.Y, bounds.Width, bounds.Height),
					new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

				// Draw with corrected X coordinate
				g.DrawImage(image, new Rectangle(bounds.X, y, bounds.Width, bounds.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
			} else {
				// Draw the image once, fully in the viewable area
				g.DrawImage(image, bounds, sourceBounds, GraphicsUnit.Pixel);
			}
		}

		#endregion

		#region "Preview updating"
		/// <summary>
		/// Modify the preview bitmap
		/// </summary>
		private void UpdatePreviewImage() {
			using (Graphics g = Graphics.FromImage(previewBitmap)) {
				//-- Set the Interpolation Mode to HQB to get a nice, smooth image
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				for (int idx = 0; idx < Screens.Length; idx++) {
					AddImageToPreview(g, GetBitmap(idx), Screens[idx].Bounds, idx);
				}
				//-- Place a red border over the selected image
				if (SelectedIndex != -1) {
					//-- Adjust Clip to entire image so the border can be highlighted
					g.Clip = new Region(previewBounds[SelectedIndex]);
					HighlightPreviewImage(g, SelectedIndex, BorderColorSelected, BorderSize);
				}
			}
		}

		private Rectangle GetPreviewBounds(Rectangle screenBounds) {
			// Translate negative screens (to the left/above primary monitor)
			// into viewable preview area
			screenBounds.X += refPoint.X;
			screenBounds.Y += refPoint.Y;

			// Everything is quarter-sized for preview mode
			screenBounds.X /= 4;
			screenBounds.Y /= 4;

			return new Rectangle(screenBounds.Location, GetPreviewSize(screenBounds.Size));
		}

		private Size GetPreviewSize(Size bounds) {
			return new Size(bounds.Width / 4, bounds.Height / 4);
		}

		/// <summary>
		/// Adds the given image to the preview image
		/// </summary>
		/// <param name="g"></param>
		/// <param name="filename"></param>
		/// <param name="bounds"></param>
		private void AddImageToPreview(Graphics g, Bitmap image, Rectangle bounds, int idx) {
			//-- Get the bounds of the preview image
			bounds = previewBounds[idx];

			//-- Portions of image that are outside of bounds will not be drawn
			g.Clip = new Region(bounds);

			// Clear the region and verify a selected filename
			g.FillRectangle(BackgroundColors[idx], bounds);

			if (image != null) {
				//-- Draw the image in it's correct location and size
				//-- Note we use the GetPreviewSize here because we want to resize the image for
				// the preview, which is 1/4 the ScreenBounds size. 
				g.DrawImage(image, ImageResizer.ResizeImage(GetPreviewSize(image.Size), bounds, Styles[idx]));
			}

			RenderCaption(g, bounds, idx);

			HighlightPreviewImage(g, idx, BorderColorStandard, BorderSize);
		}

		/// <summary>
		/// This method will place a border around the image
		/// </summary>
		/// <param name="index">Index to PreviewBounds</param>
		/// <param name="brush">Brush color to be used</param>
		/// <param name="size">Size of the pen that will be used to draw the border</param>
		private void HighlightPreviewImage(Graphics g, int index, Color color, float size) {
			g.DrawRectangle(new Pen(color, size), previewBounds[index]);
		}

		/// <summary>
		/// Render text over a Graphics object
		/// </summary>
		/// <param name="g"></param>
		/// <param name="bounds"></param>
		/// <param name="caption"></param>
		private void RenderCaption(Graphics g, Rectangle bounds, int index) {
			string caption = (index + 1).ToString();
			//-- Check if this is the primary screen
			if (Screens[index].Primary)
				caption += "p";

			//-- Create the font
			Font captionFont = new Font("Calibri", bounds.Height / 8);

			//-- Create a graphics path
			GraphicsPath path = new GraphicsPath();
			path.AddString(caption, captionFont.FontFamily,
					(int)captionFont.Style, (float)captionFont.Height,
					bounds, StringFormat.GenericDefault);

			//-- Set the smoothing mode so that the text looks smooth
			g.SmoothingMode = SmoothingMode.AntiAlias;

			//-- Draw the caption outline
			g.DrawPath(CaptionOutline, path);
			//-- Draw the caption
			g.FillPath(CaptionFill, path);
		}

		#endregion

		/// <summary>
		/// Creates a new Rectangle by translating an existing Rectangle based on a given Point
		/// </summary>
		/// <param name="r">The original rectangle</param>
		/// <param name="p">The reference point (representing 0,0)</param>
		/// <returns>The translated rectangle</returns>
		private Rectangle ZeroRectangle(Rectangle r, Point p) {
			return new Rectangle(
					r.X + p.X, r.Y + p.Y,
					r.Width + p.X, r.Height + p.Y);
		}

		/// <summary>
		/// Retrieves a cached Bitmap or reads from disk.
		/// If filename is not valid, null is retured
		/// </summary>
		/// <param name="idx">Which screen this is for (used for caching)</param>
		/// <returns>A Bitmap if found or on disk, null if an error occured</returns>
		private Bitmap GetBitmap(int idx) {
			string filename = wallpaperFilenames[idx];
			Bitmap currentImage = null;

			// Check cache first (is it there *and* alive?)
			if (wallpaperBitmapCache.ContainsKey(idx)) {
				// If it's alive, use it.  Otherwise remove it.
				if (wallpaperBitmapCache[idx].IsAlive)
					currentImage = (Bitmap)wallpaperBitmapCache[idx].Target;
				else
					wallpaperBitmapCache.Remove(idx);
			}

			// If it's not in cache but it's on disk...
			if (currentImage == null) {
				if (File.Exists(filename)) {
					using (Stream sr = File.OpenRead(filename)) {
						// Use FromStream, not FromFile to avoid an unnecessary lock
						currentImage = (Bitmap)Bitmap.FromStream(sr);
						sr.Close();

						// Save to cache as WeakReference to reduce memory footprint
						WeakReference imgRef = new WeakReference(currentImage);
						wallpaperBitmapCache.Add(idx, imgRef);
					}
				} else
					currentImage = null;
			}

			// Update the bitmap for this screen and return it
			wallpaperBitmaps[idx] = currentImage;
			return currentImage;
		}

		/// <summary>
		/// Determine the overall bounds for all monitors together and create a single Bitmap
		/// </summary>
		private void UpdateMonitorBounds() {
			Rectangle overallBounds = new Rectangle();
			refPoint = new Point();
			previewBounds = new Rectangle[Screens.Length];

			foreach (Screen scr in Screens) {
				overallBounds = AddBounds(overallBounds, scr.Bounds);
			}

			// Screens to the left or above the primary screen cause 0,0 to be other
			// than the top/left corner of the Bitmap
			if (overallBounds.X < 0) refPoint.X = Math.Abs(overallBounds.X);
			if (overallBounds.Y < 0) refPoint.Y = Math.Abs(overallBounds.Y);

			// Cancels out the negative values from offset screens
			Rectangle correctedBounds = ZeroRectangle(overallBounds, refPoint);

			//-- Initialize Preview & Desktop Bitmaps
			previewBitmap = new Bitmap(correctedBounds.Width / 4, correctedBounds.Height / 4);
			desktopBitmap = new Bitmap(correctedBounds.Width, correctedBounds.Height);

			//-- Set PreviewBounds so we can correctly map a point on the image to a particular screen index
			for (int i = 0; i < Screen.AllScreens.Length; i++) {
				previewBounds[i] = GetPreviewBounds(Screen.AllScreens[i].Bounds);
			}
		}

		private static Rectangle AddBounds(Rectangle sourceBounds, Rectangle newBounds) {
			if (newBounds.Right > sourceBounds.Right)
				sourceBounds.Width += (newBounds.Right - sourceBounds.Width);

			if (newBounds.Bottom > sourceBounds.Bottom)
				sourceBounds.Height += (newBounds.Bottom - sourceBounds.Height);

			if (newBounds.Left < sourceBounds.Left) {
				sourceBounds.X = newBounds.X;
			}

			if (newBounds.Top < sourceBounds.Top) {
				sourceBounds.Y = newBounds.Y;
			}

			return sourceBounds;
		}


		public void Update(bool updateDesktopImage, bool updatePreviewImage) {
			UpdateMonitorBounds();
			if (updatePreviewImage)
				UpdatePreviewImage();
			if (updateDesktopImage)
				UpdateDesktopImage();
		}


		#region Public Methods
		/// <summary>
		/// Returns the index of the image that corresponds to the given point on the
		/// preview image. So if there is only 1 image, this method will always return 0,
		/// permitting the point is located within the bounds of the image. If there are two
		/// images (IE 2 desktops) then this will return the zero-based index of the
		/// corresponding image.
		/// </summary>
		/// <param name="p">Locaiton in Image Coordinates</param>
		/// <returns>The index of the image that p resides in, otherwise -1 if p is outside the bounds of the image</returns>
		public int GetIndexFromPointOnPreviewImage(Point p) {
			for (int i = 0; i < previewBounds.Length; i++) {
				Rectangle r = previewBounds[i];
				if (p.X >= r.Left && p.X <= r.Right && p.Y >= r.Top && p.Y <= r.Bottom)
					return i;
			}
			return -1;
		}

		#region Setters

		public void InitScreen(WallpaperConfig config) {
			SetBackgroundColor(config.ScreenIndex, config.BackgroundColor);
			SetWallpaper(config.ScreenIndex, config.ImagePath);
			SetWallpaperStyle(config.ScreenIndex, config.StretchStyle);
		}

		private void SetWallpaper(int index, string filename) {
			if (index > -1 && index < previewBounds.Length) {
				// Update the corresponding wallpaper filename
				wallpaperFilenames[index] = filename;
				// Destroy the weak reference if it exists
				if (wallpaperBitmapCache.ContainsKey(index)) {
					Bitmap bm = (Bitmap)wallpaperBitmapCache[index].Target;
					if (bm != null) {
						bm.Dispose();
						bm = null;
					}
					wallpaperBitmapCache.Remove(index);
				}
			}
		}

		private void SetBackgroundColor(int index, Color color) {
			if (index > -1 && index < BackgroundColors.Length) {
				BackgroundColors[index].Dispose();
				BackgroundColors[index] = new SolidBrush(color);
			}
		}

		private void SetWallpaperStyle(int index, WallpaperStretchStyle style) {
			if (index > -1 && index < Styles.Length)
				Styles[index] = style;
		}

		#endregion

		#endregion


		#region IDisposable Members

		public void Dispose() {
			foreach (WeakReference wr in wallpaperBitmapCache.Values) {
				if (wr.Target != null) {
					Bitmap bm = (Bitmap)wr.Target;
					bm.Dispose();
				}
			}

			foreach (Bitmap bm in wallpaperBitmaps) {
				if (bm != null)
					bm.Dispose();
			}
		}

		#endregion
	}
}