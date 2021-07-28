using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using FlickrNet;
using ImagingService.Interfaces;

namespace ImagingService
{
    /// <summary>
    ///     Imaging service using nuget service
    /// </summary>
    public class FlickrNetImagingService : IImagingService
    {
        private readonly Flickr _flickr;

        public FlickrNetImagingService()
        {
            _flickr = new Flickr("16781e2ae34f512229bae21f884c4257");
        }

        public async Task<ICollection<string>> GetImages(string keyword)
        {
            ICollection<string> urlList = new ObservableCollection<string>();

            var searchOptions = new PhotoSearchOptions
                {Tags = keyword, PerPage = 500, Page = 1};

            try
            {
                var photoList = await Task.Run(() => _flickr.PhotosSearch(searchOptions));
                if (photoList != null)
                {
                    foreach (var photo in photoList)
                        urlList.Add(photo?.SmallUrl);
                }
                
                IsGetSuccessful = true;
                return urlList;
            }

            catch (WebException)
            {
                IsGetSuccessful = false;
                return null;
            }
        }

        public bool IsGetSuccessful { get; set; }
    }
}