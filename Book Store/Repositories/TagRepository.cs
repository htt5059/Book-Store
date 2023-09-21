using Book_Store.Data;
using Book_Store.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TagRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
        public async Task<TagModel> AddAsync(TagModel tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<TagModel> DeleteAsync(TagModel tag)
        {
            var existingTag = await _dbContext.Tags.FindAsync(tag.ID);
            if (existingTag != null) 
            {
                _dbContext.Tags.Remove(existingTag);
                _dbContext.SaveChanges();
                return existingTag;
            }
            return null;
        }

        public IEnumerable<TagModel> GetAll()
        {
            return _dbContext.Tags.ToListAsync().Result.OrderBy(x => x.DisplayName);
        }

        public async Task<TagModel> GetAsync(Guid id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }

        public async Task<TagModel> UpdateAsync(TagModel tag)
        {
            var existingTag = await _dbContext.Tags.FindAsync(tag.ID);
            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _dbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }
    }
}
