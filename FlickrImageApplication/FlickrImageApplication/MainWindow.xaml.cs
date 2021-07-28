using System.Windows;
using Microsoft.Practices.Unity;

namespace FlickrImageApplication
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();

            var imageSearchUserControl = container.Resolve<ImageSearchUserControl>();
            ImageSearchUserContentControl.Content = imageSearchUserControl;
        }
    }
}