using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Domains
{
    public class TagModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogModel>? Blogs { get; set; }
    }
}
