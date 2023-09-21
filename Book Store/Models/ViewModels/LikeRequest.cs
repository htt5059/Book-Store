using System;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models.ViewModels
{
    public class LikeRequest
    {
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
    }
}
