using Ninject;
using System;
using System.Windows.Forms;

namespace WallpaperChanger
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                log4net.Config.XmlConfigurator.Configure();

                IKernel kernel = new StandardKernel(new WallMasterModule());
                ProgramRunner pr = kernel.Get<ProgramRunner>();

                pr.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}