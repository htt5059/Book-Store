using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Book_Store.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {   
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file) 
        {
            var imageUrl = await _imageRepository.UploadImageAsync(file);

            if (imageUrl == null)
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            return new JsonResult(new { link = imageUrl });
        }
    }
}
