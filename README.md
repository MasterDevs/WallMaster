# WallMaster

Wallmaster allows a user to set different wallpapers for each monitor in a multi-monitor setup.
It is written in .Net 3.5

![Screen Shot](https://raw.githubusercontent.com/MasterDevs/WallMaster/master/ScreenShot.png)

It provides the following features:
* Completely independent selection of wallpapers for each screen
* Random selection of wallpapers based on a user defined directory
* User defined interval for changing wallpapers
* Selection of a different background color for different monitors
* Multiple options for placing a wallpaper on a screen
   * Center 
      * Centers image, cropping it to the limitations of the screen
   * Stretch 
      * Stretch the image to take up the entire screen, distorting aspect ratio
   * StretchRatio
      * Stretch the image to take up the entire screen, maintaining aspect ratio
      * This may result in either horizontal or vertical bars around the image
   * Center Fit
      * Center an image when it is smaller than the screen, otherwise use StretchRatio
* Dynamically discovers screen the number of screens
* Resize images automatically when the screen resolution changes
* Hides in notification tray on minimize
* Supports Windows 8

### Windows8 Update
There is an issue when using multiple synced instances of Windows8 all running WallMaster at the same time.  When an instance updates the wallpaper it willy automatically sync and update the each others.  The problem is exacerbated if the two computers don't have the the same number and orientation of monitors.

The work around is to disable synchronization of wallpapers.

0. Bring up charms (win+c)
0. Go to "Change PC Settings" (at the bottom)
0. Go to "sync your settings"
0. Disable desktop personalization 

Unfortunately, personalization is an all or nothing affair.
