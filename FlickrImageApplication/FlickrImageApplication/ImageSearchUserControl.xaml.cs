using System.Windows.Controls;
using System.Windows.Data;
using FlickrImageApplication.ViewModel;
using Microsoft.Practices.Unity;

namespace FlickrImageApplication
{
    /// <summary>
    ///     Interaction logic for ImageSearchUserControl.xaml
    /// </summary>
    public partial class ImageSearchUserControl : UserControl
    {
        public ImageSearchUserControl(IUnityContainer container)
        {
            DataContext = container.Resolve<FlickrUiViewModel>();
            InitializeComponent();
        }

        private void ImageItemControl_OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            ImageScrollViewer.ScrollToTop();
        }
    }
}