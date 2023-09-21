using Book_Store.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
    public class TagRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public RequestAction Action { get; set; }
    }
}
