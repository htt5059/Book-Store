using Book_Store.Models;
using Book_Store.Models.ViewModels;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogLikesRepository  _likesRepository;

        public HomeController(ILogger<HomeController> logger,IBlogLikesRepository blogLikesRepository, IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
            _likesRepository = blogLikesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllAsync();
            List<BlogVM> blogVMs = new List<BlogVM>();
            foreach (var blog in blogs) {
                if (blog != null) {
                    var totalLikes = await _likesRepository.GetTotalLikes(blog.Id);
                    blogVMs.Add(new BlogVM {
                        Blog = blog,
                        BlogLikes = totalLikes
                    });
                }
            }
            return View(blogVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
