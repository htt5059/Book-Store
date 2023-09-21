using Book_Store.Data;
using Book_Store.Models.Domains;
using Book_Store.Models.ViewModels;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogAdapter _blogAdapter;
        public AdminBlogController(ITagRepository tagRepository, IBlogRepository blogRepository, IBlogAdapter blogAdapter)
        {
            _tagRepository = tagRepository;
            _blogRepository = blogRepository;
            _blogAdapter = blogAdapter;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Blog()
        {
            var tags = _tagRepository.GetAll().ToList();
            for (int i = 0; i < tags.Count; i++) {
                if (tags[i].DisplayName == null)
                    tags.RemoveAt(i--);
            }

            var model = new BlogVM {
                BlogRequest = new BlogRequest {
                    Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() })
                }
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("AdminBlog/Blog/{id}")]
        public async Task<IActionResult> Blog(Guid id)
        {
            var blog = await _blogRepository.GetAsync(id);
            var blogVm = new BlogVM {
                Action = RequestAction.Update,
                Blog = blog,
                BlogRequest = _blogAdapter.ToBlogRequest(blog),
            };
            var tags = _tagRepository.GetAll().ToList().Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() });
            blogVm.BlogRequest.Tags = tags;
            return View("Blog", blogVm);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("AdminBlog/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid Id) 
        {
            await _blogRepository.DeleteAsync(Id);
            return RedirectToAction("Blogs");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Blog(BlogVM blogVM) 
        {
            await _blogRepository.AddAsync(await _blogAdapter.ToBlogModel(blogVM.BlogRequest));
            return RedirectToAction("Blogs");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Update(BlogVM blogVM) 
        {
            var blog = await _blogAdapter.ToBlogModel(blogVM.BlogRequest);
            await _blogRepository.UpdateAsync(blog);
            return RedirectToAction("Blogs");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> Blogs() 
        {
            var blogList = await _blogRepository.GetAllAsync();
            return View(blogList);
        }
    }
}
