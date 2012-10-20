namespace WallpaperUtils
{
    /// <summary>
    /// Determines whether or not an image is stretched/rotated/centered
    /// This is used when the PaneledImageBuilder to determine how to create
    /// the image.
    /// </summary>
    public enum WallpaperStretchStyle
    {
        /// <summary>
        /// Center image, if it is larger than the destination screen
        /// then crop the image at the bounds of the screen
        /// </summary>
        Center = 0,

        /// <summary>
        /// Image is centered unless it is larger then the desktop.
        /// If it's larger then the desktop, it will be fit,
        /// preserving it's aspect ratio.
        /// </summary>
        CenterFit = 1,

        /// <summary>
        /// Stretch the image to fit the destination screen.
        /// <para>If the image is larger or smaller then the destination, it will
        /// be resized so the entire image appears on the destination screen</para>
        /// </summary>
        Stretch = 2,

        /// <summary>
        /// If the image is larger than the size of the screen
        /// then resize it to fit; aspect ratio is maintained,
        /// </summary>
        Fit = 3,

        /// <summary>
        /// If an image is smaller than the screen, then center it,
        /// otherwise, StretchRatio it.
        /// </summary>
        Fill = 4,
    }
}