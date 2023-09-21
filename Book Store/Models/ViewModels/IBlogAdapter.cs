using Book_Store.Models.Domains;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
    public interface IBlogAdapter
    {
        Task<BlogModel> ToBlogModel(BlogRequest blogRequest);
        BlogRequest ToBlogRequest(BlogModel blog);
    }
}
