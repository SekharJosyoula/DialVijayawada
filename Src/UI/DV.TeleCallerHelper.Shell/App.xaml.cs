using System.Windows;
using DV.TeleCallerHelper.Shell;

namespace DV.TeleCallerHelper.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure Bootstrapper
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
