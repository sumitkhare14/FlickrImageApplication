using System.Windows;
using Microsoft.Practices.Unity;

namespace FlickrImageApplication
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            var mainWindow = container.Resolve<MainWindow>(); // Creating Main window
            mainWindow.Show();
        }
    }
}