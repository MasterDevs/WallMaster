using System;
using System.Windows.Forms;

namespace WallpaperChanger {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Exit();
			//Application.Run(new WallmasterUI());
			//Application.Run(new TestForm());
			Application.Run(new SimpleTestForm());
		}
	}
}
