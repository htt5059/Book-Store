using Book_Store.Models.Domains;
using OpenAI_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_Store.Repositories
{
    public interface IOpenAIRepository
    {
        Task<string> SendMessageAsync(string user, string message, Model model = null, double temp = 0.1, int maxToken = 50);
        Task<List<ChatMessageModel>> GetChatLog();
    }
}
