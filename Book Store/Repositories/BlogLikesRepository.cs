using Book_Store.Data;
using Book_Store.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public class BlogLikesRepository : IBlogLikesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogLikesRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
        public async Task<int> GetTotalLikes(Guid blogId)
        {
            return await _dbContext.Likes.CountAsync(x => x.BlogId == blogId);
        }

        public async Task<BlogLikes> AddLike(BlogLikes like) { 
            await _dbContext.Likes.AddAsync(like);
            await _dbContext.SaveChangesAsync();
            return like;
        }
    }
}
