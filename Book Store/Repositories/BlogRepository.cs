using Book_Store.Data;
using Book_Store.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BlogModel> AddAsync(BlogModel blog)
        {
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<BlogModel> DeleteAsync(Guid Id)
        {
            var existingBlog = await _dbContext.Blogs.FindAsync(Id);
            if (existingBlog != null) 
            {
                _dbContext.Blogs.Remove(existingBlog);
                _dbContext.SaveChanges();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogModel>> GetAllAsync()
        {
            var blogList = await _dbContext.Blogs.Include(x => x.Tags).ToListAsync();
            return blogList;
        }

        public async Task<BlogModel> GetAsync(Guid id)
        {
            var blog = await _dbContext.Blogs.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
            return blog;
        }

        public async Task<BlogModel> GetAsync(string urlHandle) 
        {
            var blog = await _dbContext.Blogs.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandled == urlHandle);
            return blog;
        }

        public async Task<BlogModel> UpdateAsync(BlogModel blog)
        {
            var existingBlog = await GetAsync(blog.Id);
            if (existingBlog != null) 
            {
                existingBlog.Heading = blog.Heading;
                existingBlog.PageTitle = blog.PageTitle;
                existingBlog.PublishDate = blog.PublishDate;
                existingBlog.ShortDescription = blog.ShortDescription;
                existingBlog.Content = blog.Content;
                existingBlog.UrlHandled = blog.UrlHandled;
                existingBlog.FeaturedUrl = blog.FeaturedUrl;
                existingBlog.Visible = blog.Visible;
                if (existingBlog.Tags != blog.Tags) 
                {
                    existingBlog.Tags = blog.Tags;
                }
                existingBlog.Author = blog.Author;
                await _dbContext.SaveChangesAsync();
            }
            return existingBlog;
        }
    }
}
