using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using FlickrImageApplication.Commands;
using FlickrImageModel;
using ImagingService.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace FlickrImageApplication.ViewModel
{
    /// <summary>
    ///     ViewModel which manages the display of images
    /// </summary>
    public class FlickrUiViewModel : INotifyPropertyChanged
    {
        private IUnityContainer _container;
        private readonly IImagingService _imageServiceService;
        private string _searchString;

        private FlickrUiModel _uiModel;

        public FlickrUiViewModel(IUnityContainer container)
        {
            _container = container;
            RegisterDependencies();
            _imageServiceService = container.Resolve<IImagingService>();
            UiModel = container.Resolve<FlickrUiModel>();
            SearchImageCommand = new Command(ExecuteSearchImageCommand, CanExecuteSearchImageCommand);
            ClearSearchResultsCommand = new Command(ExecuteClearSearchResultsCommand);
        }

        public Command SearchImageCommand { get; }

        public Command ClearSearchResultsCommand { get; }

        public FlickrUiModel UiModel
        {
            get => _uiModel;

            set
            {
                if (_uiModel != value)
                {
                    _uiModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UiModel"));
                }
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                if (value != _searchString)
                {
                    _searchString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchString"));
                    SearchImageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsFetchSuccessful { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteClearSearchResultsCommand(object obj)
        {
            UiModel.PhotoCollection?.Clear();
            SearchString = string.Empty;
        }

        private bool CanExecuteSearchImageCommand()
        {
            return !string.IsNullOrEmpty(SearchString);
        }

        private async void ExecuteSearchImageCommand(object obj)
        {
            if (UiModel != null)
            {
                UiModel.PhotoCollection?.Clear();

                var photoList = await _imageServiceService.GetImages(SearchString);
                UiModel.PhotoCollection = (ObservableCollection<string>) photoList;
                IsFetchSuccessful = _imageServiceService.IsGetSuccessful;
            }
        }
        
        private void RegisterDependencies()
        {
            var configuration =
                ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            var unityConfigurationSection = (UnityConfigurationSection) configuration.GetSection("unity");
            _container.LoadConfiguration(unityConfigurationSection);
        }

    }
}