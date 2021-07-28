using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FlickrImageModel
{
    /// <summary>
    ///     Data model interface for displaying the photos
    /// </summary>
    public class FlickrUiModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _photoCollection;

        public ObservableCollection<string> PhotoCollection
        {
            get => _photoCollection;
            set
            {
                _photoCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhotoCollection"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}