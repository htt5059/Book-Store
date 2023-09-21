using Book_Store.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogModel>> GetAllAsync();
        Task<BlogModel?> GetAsync(Guid id);
        Task<BlogModel> AddAsync(BlogModel blog);
        Task<BlogModel?> UpdateAsync(BlogModel blog);
        Task<BlogModel?> DeleteAsync(Guid Id);
        Task<BlogModel?> GetAsync(string urlHandle);
    }
}
