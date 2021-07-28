using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using ImagingService.Interfaces;
using Newtonsoft.Json;

namespace ImagingService
{
    /// <summary>
    ///     Imaging service using http api
    /// </summary>
    public class FlickrHttpApiImagingService : IImagingService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public FlickrHttpApiImagingService()
        {
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
        }

        public async Task<ICollection<string>> GetImages(string keyword)
        {
            var flickrResponse = await Task.Run(() => GetResource(keyword));

            ICollection<string> urlList = new ObservableCollection<string>();

            if (flickrResponse != null)
            {
                foreach (var imageItem in flickrResponse.Items)
                    urlList.Add(imageItem.Media.M);
            }
            
            return urlList;
        }

        public bool IsGetSuccessful { get; set; }

        private async Task<FlickrDataResponseModel> GetResource(string keyword)
        {
            try
            {
                var requestUri =
                    $"http://api.flickr.com/services/feeds/photos_public.gne?tags={keyword}&per_page=5&tagmode=any&format=json&nojsoncallback=1?";

                FlickrDataResponseModel flickrDataModel = null;

                var response = await _httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                flickrDataModel =
                    JsonConvert.DeserializeObject<FlickrDataResponseModel>(content);

                IsGetSuccessful = true;
                return flickrDataModel;
            }
            catch (HttpRequestException)
            {
                IsGetSuccessful = false;
                return null;
            }
        }
    }
}