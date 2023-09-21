using Book_Store.Models.ViewModels;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Book_Store.Views.Blog
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogLikesRepository _blogLikesRepository;

        public BlogController(IBlogRepository blogRepository, IBlogLikesRepository blogLikesRepository) {
            _blogRepository = blogRepository;
            _blogLikesRepository = blogLikesRepository;
        } 

        [HttpGet]
        public async Task<IActionResult> Blog(string urlHandle)
        {
            var blog = await _blogRepository.GetAsync(urlHandle);
            if (blog != null) {
                var totalLikes = await _blogLikesRepository.GetTotalLikes(blog.Id);
                var blogVM = new BlogVM {
                    Blog = blog,
                    BlogLikes = totalLikes
                };
                return View(blogVM);
            }
            return View(new BlogVM { Blog = blog});
        }
    }
}
