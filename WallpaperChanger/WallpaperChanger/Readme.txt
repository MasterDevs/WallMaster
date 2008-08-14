Wallmaster allows a user to set different wallpapers for each monitor in a multi-monitor setup.
It is written in .Net 3.5

It provides the following features:
   - Completely independant selection of wallpapers for each screen
   - Random selection of wallpapers based on a user defined directory
   - User defined interval for changing wallpapers
   - Selection of a different background color for different monitors
   - Multiple options for placing a wallpaper on a screen
      - Center 
         - Centers image, cropping it to the limitations of the screen
      - Stretch 
         - Stretch the image to take up the entire screen, distorting aspect ratio
      - StretchRatio
         - Stretch the image to take up the entire screen, maintaining aspect ratio
         - This may result in either horizontal or vertical bars around the image
      - Magic
         - Center an image when it is smaller than the screen, otherwise use StretchRatio
   - Dynamically discovers screen the number of screens
   - Resizes images automatically when the screen resolution changes
   - Hides in notification tray on minimize