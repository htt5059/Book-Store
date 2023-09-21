using Book_Store.Models.Domains;
using Book_Store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<TagModel> GetAll();
        Task<TagModel?> GetAsync(Guid id);
        Task<TagModel> AddAsync(TagModel tag);
        Task<TagModel?> UpdateAsync(TagModel tag);
        Task<TagModel?> DeleteAsync(TagModel tag);
    }
}
