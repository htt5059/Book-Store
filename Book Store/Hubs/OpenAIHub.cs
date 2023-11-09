using Book_Store.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Book_Store
{
    public class OpenAIHub : Hub
    {
        private IOpenAIRepository _openAIRepository;
        public OpenAIHub(IOpenAIRepository openAI) 
        {
            _openAIRepository = openAI;
        }
        public async Task SendMessageAsync(string user, string message) 
        {
            message = await _openAIRepository.SendMessageAsync(user, message);
            await Clients.Caller.SendAsync("RecieveMessage", message);
        }
    }
}
