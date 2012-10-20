namespace WallpaperUtils
{
    /// <summary>
    /// Determines how the application chooses a wallpaper
    /// </summary>
    public enum WallpaperSelectionStyle
    {
        /// <summary>
        /// No wallpaper, just background color
        /// </summary>
        None = 0,

        /// <summary>
        /// Use the image in the specified file
        /// </summary>
        File = 1,

        /// <summary>
        /// Use a randomly selected image from a specified directory
        /// </summary>
        Random = 2,
    }
}