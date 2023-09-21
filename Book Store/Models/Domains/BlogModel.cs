using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Domains
{
    public class BlogModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string? ShortDescription { get; set; }
        public string FeaturedUrl { get; set; }
        public string UrlHandled { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<TagModel> Tags { get; set; }
        public ICollection<BlogLikes> Likes { get; set; }
    }
}
