using System;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context) => _context = context;
        
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            await _context.AddAsync(new Models.Chat{
                Name = name,
                ChatType = ChatType.Group
            });

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
                Text = message,
                TimeStamp = DateTime.Now
            };

        await _context.Messages.AddAsync(Message);
        await _context.SaveChangesAsync();
        return RedirectToAction("GetChat", new { id = Id});
        }
    }    
}