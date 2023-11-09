using System;

namespace Book_Store.Models.Domains
{
    public class ChatMessageModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
