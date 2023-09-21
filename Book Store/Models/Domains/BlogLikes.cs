using System;

namespace Book_Store.Models.Domains
{
    public class BlogLikes
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
    }
}
