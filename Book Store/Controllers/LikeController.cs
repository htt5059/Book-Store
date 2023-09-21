using Book_Store.Migrations;
using Book_Store.Models.Domains;
using Book_Store.Models.ViewModels;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IBlogLikesRepository _blogLikeRepository;

        public LikeController(IBlogLikesRepository blogLikesRepository) {
            _blogLikeRepository = blogLikesRepository;
        }

        [HttpGet]
        [Route("GetLikes/{BlogId}")]
        public async Task<IActionResult> GetLikes(Guid BlogId) {
            var totalLikes = await _blogLikeRepository.GetTotalLikes(BlogId);
            
            return Ok(new BlogVM { BlogLikes = totalLikes});
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddBlogLikestoDb([FromBody]BlogLikes addLikeRequest) {
            var model = new BlogLikes {
                BlogId = addLikeRequest.BlogId,
                UserId = addLikeRequest.UserId
            };
            await _blogLikeRepository.AddLike(model);
            return Ok(model);
        }
    }
}
