using Ninject.Modules;
using WallpaperUtils;

namespace WallpaperChanger
{
    public class WallMasterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ProgramRunner>().ToSelf().InSingletonScope();
            Bind<QuickChanger>().ToSelf().InSingletonScope();
            Bind<WallpaperConfigChanger>().ToSelf().InSingletonScope();
            Bind<WallpaperChangerForm>().ToSelf().InSingletonScope();
            Bind<WallpaperConfigManager>().ToSelf().InSingletonScope();
            Bind<WallpaperCreator>().ToSelf().InSingletonScope();
            Bind<WallpaperManager>().ToSelf().InSingletonScope();
        }
    }
}