using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace $safeprojectname$
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		/// <summary>
		/// The entry point in the application
		/// </summary>
		[STAThread]
        public static void Main()
        {
            var application = new App();
            application.InitializeComponent();
			
			// Load localizations from the disk
            LanguagesLoader.Init();
			
            application.Run();
        }
    }
}
