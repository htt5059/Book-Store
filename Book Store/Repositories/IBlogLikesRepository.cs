using Book_Store.Models.Domains;
using System;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface IBlogLikesRepository
    {
        Task<int> GetTotalLikes(Guid blogId);
        Task<BlogLikes> AddLike(BlogLikes like);
    }
}
