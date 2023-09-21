using Book_Store.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
    public class TagVM
    {
        public TagModel Tag { get; set; }
        public TagRequest TagRequest { get; set; }
        public List<TagModel> TagList { get; set; }
        public Guid TagId { get; set; }
    }
}
