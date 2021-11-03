using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context) => _context = context;
        
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Models.Chat{
                Name = name,
                ChatType = ChatType.Group
            };

            chat.Users.Add(new ChatUser {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,       //i don't what is happening here
                Role = UserRole.Admin
            });

            await _context.AddAsync(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id) => 
            View("Index",await _context.Chats
            .Include(m => m.Messages)
            .FirstOrDefaultAsync(m => m.Id == id));                


        [HttpPost]
        public async Task<IActionResult> SendMessage(int Id, string message){
            var Message = new Message()
            {
                ChatId = Id,
                UserName = User.Identity.Name,
                Text = message,
                TimeStamp = DateTime.Now
            };

        await _context.Messages.AddAsync(Message);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetChat", new { id = Id});
        }
    }    
}