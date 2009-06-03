using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WallpaperChanger {
	public partial class DesktopPreview : PictureBox {

		//private Bitmap PreviewBitmap;
		//private Bitmap DesktopBitmap;
		Dictionary<string, WeakReference> wallpaperBitmapCache = new Dictionary<string, WeakReference>();
		Dictionary<string, Bitmap> wallpaperBitmaps = new Dictionary<string, Bitmap>();

		public DesktopPreview() {
		}

		public void InitBox(Resolution.Resolution[] resolutions) {
			//-- GET CURRENT RESOLUTIONS ?? 
			Rectangle totalBounds = resolutions[0].Bounds;
			//for (int i = 1; i < resolutions.Length; i++) {
			//  totalBounds = AddBounds(totalBounds, resolutions[i].Bounds);
			//}

		}
	}
}
