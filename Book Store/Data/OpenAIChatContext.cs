using Book_Store.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data
{
    public class OpenAIChatContext : DbContext
    {
        public OpenAIChatContext(DbContextOptions<OpenAIChatContext> options) : base(options)
        { }

        public DbSet<ChatMessageModel> ChatMessages { get; set; }
    }
}
