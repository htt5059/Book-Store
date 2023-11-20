using Book_Store.Data;
using Book_Store.Models.Domains;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Book_Store.Repositories
{
    public class OpenAIRepository : IOpenAIRepository
    {
        private readonly IConfiguration _configuration;
        private OpenAIAPI _openAI;
        private Conversation _conversation;
        private IHttpContextAccessor _context;
        private OpenAIChatContext _openAIDbContext;
        private AuthDbContext _authDbContext;
        private IdentityUser _user;
        private readonly ILogger _logger;


        public OpenAIRepository(IConfiguration configuration,
            IHttpContextAccessor httpContext,
            OpenAIChatContext openAIDbContext,
            AuthDbContext authDbContext,
            SignInManager<IdentityUser> signInManager,
            ILogger<IOpenAIRepository> logger)
        {
            _configuration = configuration;
            _context = httpContext;
            _openAIDbContext = openAIDbContext;
            _authDbContext = authDbContext;
            _logger = logger;

            var authentication = new APIAuthentication(Environment.GetEnvironmentVariable("Dong-A_OpenAI-API-Key"));
            _openAI = new OpenAIAPI(authentication);
            _conversation = _openAI.Chat.CreateConversation();
        }

        public async Task<string> SendMessageAsync(string user,
            string message,
            Model model = null,
            double temp = 0.5,
            int maxToken = 2048)
        {
            _user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.UserName == user);
            string userId = _user != null ? _user.Id : "";

            if (user == "Guest") 
            {
                try {
                    _conversation.AppendUserInput(message);
                    var res = await _conversation.GetResponseFromChatbotAsync();
                    return res.Replace("\n", "<br>");
                }
                catch (Exception e) {
                    _logger.LogError(e.Message);
                    return "";
                }
            }
            else 
            {
                _openAIDbContext.ChatMessages.Add(new ChatMessageModel() {
                    Role = "User",
                    UserId = new Guid(userId),
                    Content = message,
                    Name = _user.UserName,
                    CreatedOn = DateTime.Now
                });
                await _openAIDbContext.SaveChangesAsync();

                var res = await FeedChatLogAsync(message);
                
                if (res == null) {
                    _logger.LogWarning("Failed to get response for message:\n" +
                        $"- {message}\n"+
                        $"Check FeedChatLog() and _openAI.Chat.CreateChatCompletionAsync()");
                    return "";
                }
                //string reply = res.Choices[0].Message.Content.Replace("\n", "<br>");
                string reply = res.Replace("\n", "<br>");

                _openAIDbContext.ChatMessages.Add(new ChatMessageModel() {
                    Role = "Assistant",
                    UserId = new Guid(userId),
                    Content = reply,
                    Name = "OpenAI_" + (model == null? "ChatConversation" : model.ToString()),
                    CreatedOn = DateTime.Now
                });
                _openAIDbContext.SaveChanges();
                
                
                return reply;
            }
        }

        public async Task<List<ChatMessageModel>> GetChatLog() {
            var chatLog = await GetChatLogRaw();
            if (chatLog == null)
                return new List<ChatMessageModel>();
            return chatLog.ToList();
        }

        private async Task<IQueryable<ChatMessageModel>> GetChatLogRaw() {
            var user = _context.HttpContext.User.Identity.Name;
            _user = _authDbContext.Users.FirstOrDefault(x => x.UserName == user);
            if (_user == null)
                return null;


            return _openAIDbContext.ChatMessages.Where(x => x.UserId == new Guid(_user.Id)).Select(x => new ChatMessageModel {
                Role = x.Role,
                Content = x.Content
            });
        }

        private async Task<string> FeedChatLogAsync(string newMessage = "") {
            if (_user == null)
                return null;

            var chatLog = await GetChatLogRaw();
            //var chatContext = chatLog.ToList().OrderByDescending(x=> x.CreatedOn).Take(5);
            var chatContext = chatLog.ToList().OrderByDescending(x => x.CreatedOn);
            if (chatContext.IsNullOrEmpty())
                return null;

            List<ChatMessage> chatHistory = new List<ChatMessage>();
            _conversation.AppendMessage(new ChatMessage(ChatMessageRole.System, "For context, you're Blog AI Assistance who wrtites blogs according to user guidance, and uses statistic number from your database"));
            
            foreach (var chat in chatContext) {
                if (chat.Role == "Assistant")
                    _conversation.AppendMessage(new ChatMessage(ChatMessageRole.Assistant, chat.Content));
                //chatHistory.Add(new ChatMessage(ChatMessageRole.Assistant, chat.Content));
                else
                    _conversation.AppendMessage(new ChatMessage(ChatMessageRole.User, chat.Content));
                    //chatHistory.Add(new ChatMessage(ChatMessageRole.User, chat.Content));
            }

            if(newMessage != "")
                chatHistory.Add(new ChatMessage(ChatMessageRole.User, newMessage));

            try {
                //var res = await _openAI.Chat.CreateChatCompletionAsync(new ChatRequest() {
                //    Model = Model.ChatGPTTurbo,
                //    Temperature = 0.5,
                //    MaxTokens = 1024,
                //    Messages = chatHistory
                //});
                var res = await _conversation.GetResponseFromChatbotAsync();
                
                return res;
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
