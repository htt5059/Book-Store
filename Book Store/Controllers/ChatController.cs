using Book_Store.Models.Domains;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Chat;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IOpenAIRepository _openAI;

        public ChatController(IOpenAIRepository openAI)
        {
            _openAI = openAI;
        }

        [HttpGet]
        [Route("GetChatLog")]
        public async Task<IActionResult> GetChatLog() {
            List<ChatMessageModel> chatLog = await _openAI.GetChatLog();
            for (int i = 0; i < chatLog.Count; i++) {
                chatLog[i].Content = chatLog[i].Content.Replace("\n", "<br>");
            }
            
            return Ok(chatLog);
        }
    }
}
