using Ninject.Extensions.Logging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WallpaperUtils
{
    public class ImageSaver
    {
        private const string WP_FILE = @"wallpaper";
        private static readonly ImageFormat IMAGE_FORMAT = ImageFormat.Png;
        private static readonly ImageFormat IMAGE_FORMAT_LEGACY = ImageFormat.Bmp;
        private readonly string _appDir;
        private readonly ImageFormat _imageFormat;
        private readonly string _wallpaperPath;
        private readonly ILogger _logger;

        public ImageSaver(ILogger logger, string appDir)
        {
            _logger = logger;
            _appDir = appDir;
            _imageFormat = GetImageFormat();
            _wallpaperPath = GetWallpaperPath();
        }

        public string Save(Bitmap bitmap)
        {
            _logger.Debug("Saving image to {0} as {1}", _wallpaperPath, _imageFormat);
            bitmap.Save(_wallpaperPath, _imageFormat);
            return _wallpaperPath;
        }

        private ImageFormat GetImageFormat()
        {
            var version = Environment.OSVersion.Version;

            // Windows 8 is version:  6.2.  http://msdn.microsoft.com/en-us/library/windows/desktop/ms724832(v=vs.85).aspx
            ImageFormat format = (version.Major >= 6 && version.Minor >= 2)
                ? IMAGE_FORMAT
                : IMAGE_FORMAT_LEGACY;

            _logger.Info("OS Version is {0} so the image format is {1}", version, format);
            return format;
        }

        private string GetWallpaperPath()
        {
            string fileName = string.Format("{0}.{1}", WP_FILE, _imageFormat);
            return Path.Combine(_appDir, fileName);
        }
    }
}