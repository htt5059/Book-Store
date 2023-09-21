using Book_Store.Models.Domains;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
    public class BlogAdapter : IBlogAdapter
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogRepository _blogRepository;

        public BlogAdapter(ITagRepository tagRepository, IBlogRepository blogRepository) {
            _tagRepository = tagRepository;
            _blogRepository = blogRepository;
        }
    public async Task<BlogModel> ToBlogModel(BlogRequest blogRequest)
        {
            List<TagModel> tags = new List<TagModel>();
            foreach (var tagId in blogRequest.SelectedTags) 
            {
                var tag = await _tagRepository.GetAsync(new Guid(tagId));
                tags.Add(tag);
            }

            return new BlogModel {
                Id = blogRequest.Id,
                Heading = blogRequest.Heading,
                PageTitle = blogRequest.PageTitle,
                Visible = blogRequest.Visible,
                Author = blogRequest.Author,
                ShortDescription = blogRequest.ShortDescription,
                Content = blogRequest.Content,
                UrlHandled = blogRequest.UrlHandled,
                FeaturedUrl = blogRequest.FeaturedUrl,
                PublishDate = blogRequest.PublishDate,
                Tags = tags
            };
        }

        public BlogRequest ToBlogRequest(BlogModel blog) 
        {
            IEnumerable<SelectListItem> tags = _tagRepository.GetAll()
                    .Where(x => x.DisplayName != null)
                    .OrderBy(x => x.Name)
                    .Select(x => new SelectListItem { Text = x.DisplayName, Value = x.ID.ToString() });
            var selectedTag = blog.Tags.Select(x => x.ID.ToString()).ToArray();
            var blogRequest = new BlogRequest {
                Id = blog.Id,
                Heading = blog.Heading,
                PageTitle = blog.PageTitle,
                Visible = blog.Visible,
                Author = blog.Author,
                ShortDescription = blog.ShortDescription,
                Content = blog.Content,
                UrlHandled = blog.UrlHandled,
                FeaturedUrl = blog.FeaturedUrl,
                PublishDate = blog.PublishDate,
                Tags = tags,
                SelectedTags = selectedTag
            };
            return blogRequest;
        }
    }
}
