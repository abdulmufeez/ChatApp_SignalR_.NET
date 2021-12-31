using System;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SignalRChatController : Controller
    {
        public IHubContext<ChatHub> _hubContext;
        public SignalRChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("[action]/{connectionId}/{chatId}")]
        public async Task<IActionResult> JoinGroup(string connectionId, string chatId)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, chatId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{chatId}")]
        public async Task<IActionResult> LeaveGroup(string connectionId, string chatId)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            int chatId,
            string message, 
            [FromServices] AppDbContext _context)       //by doing this only this action use DbContext
        {
             var Message = new Message()
            {
                ChatId = chatId,
                UserName = User.Identity.Name,
                Text = message,
                TimeStamp = DateTime.Now
            };

            await _context.Messages.AddAsync(Message);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.Group(chatId.ToString())
                .SendAsync("MessageRecieve", Message);
                // .SendAsync("MessageRecieve", new { 
                //     Text = Message.Text,
                //     Name = Message.UserName,
                //     TimeStamp = Message.TimeStamp.ToShortDateString()
                // });

            return Ok(); 
        }
    }
}