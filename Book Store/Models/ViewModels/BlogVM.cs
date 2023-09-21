using Book_Store.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
    public class BlogVM
    {
        public BlogModel Blog { get; set; }
        public BlogRequest BlogRequest { get; set; }
        public int BlogLikes { get; set; }
        public RequestAction Action { get; set; }
    }
}
