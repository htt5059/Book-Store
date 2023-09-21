using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
