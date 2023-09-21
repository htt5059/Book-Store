using Book_Store.Models.Domains;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Book_Store.Models.ViewModels
{
    public class AccountVM
    {
        public AccountModel Account { get; set; }
        public string Result { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
