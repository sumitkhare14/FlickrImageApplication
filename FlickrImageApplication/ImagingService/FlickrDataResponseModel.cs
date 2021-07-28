using System.Collections.Generic;

namespace ImagingService
{
    /// <summary>
    ///     Data Model for JSON response
    /// </summary>
    public class FlickrDataResponseModel
    {
        public FlickrDataResponseModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public Items(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public Media Media { get; set; }
    }

    public class Media
    {
        public string M { get; set; }
    }
}