using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<BlogLikes> Likes { get; set; }
        public DbSet<TagModel> Tags { get; set; }
    }
}
