using Ninject.Modules;
using System;
using System.IO;
using WallpaperUtils;

namespace WallpaperChanger
{
    public class WallMasterModule : NinjectModule
    {
        private const string APP_FOLDER = @"Wallmaster";

        public override void Load()
        {
            Bind<ProgramRunner>().ToSelf().InSingletonScope();
            Bind<QuickChanger>().ToSelf().InSingletonScope();
            Bind<WallpaperChangerForm>().ToSelf().InSingletonScope();
            Bind<WallpaperConfigChanger>().ToSelf().InSingletonScope();
            Bind<WallpaperConfigManager>().ToSelf().InSingletonScope();
            Bind<WallpaperCreator>().ToSelf().InSingletonScope();
            Bind<WallpaperManager>().ToSelf().InSingletonScope();
            Bind<ImageSaver>().ToSelf().InSingletonScope();
            Bind<StartWithWindowsHelper>().ToSelf().InSingletonScope();

            string appDir = GetAppDir();
            Bind<string>().ToConstant(appDir);
        }

        public static string GetAppDir()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string path = Path.Combine(appData, APP_FOLDER);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
