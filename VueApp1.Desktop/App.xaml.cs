using System.Windows;

namespace VueApp1.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var app = Server.Program.BuildWebApplication();
            app.RunAsync();
        }
    }

}
