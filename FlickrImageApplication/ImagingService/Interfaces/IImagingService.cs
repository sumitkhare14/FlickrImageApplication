using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImagingService.Interfaces
{
    public interface IImagingService
    {
        bool IsGetSuccessful { get; }
        Task<ICollection<string>> GetImages(string keyword);
    }
}