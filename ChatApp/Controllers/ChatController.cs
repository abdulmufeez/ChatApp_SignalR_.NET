using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context) => _context = context;
        
        public IActionResult Index() => View();

        public async Task<IActionResult> CreateRoom(string name)
        {
            await _context.AddAsync(new Models.Chat{
                Name = name,
                ChatType = ChatType.Group
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}