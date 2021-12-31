using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers{    
    [Authorize]
    public class HomeController : Controller
    {
         private readonly AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;       

        //listing only public group
        public async Task<IActionResult> Index()
        {
            var chatsInDb = await _context.Chats
                .Include(model => model.Users)
                .Where(model => model.ChatType == ChatType.Group)   
                .ToListAsync();

            return View("Index", chatsInDb);
        }
        // public async Task<IActionResult> Index() 
        // {
        //     var chatsInDb = await _context.Chats
        //         .Include(x => x.Users)
        //         .Where(model => model.Users
        //             .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //         .ToListAsync();            
            
        //     return View(chatsInDb);
        // }

        //listing all users for private group
        public async Task<IActionResult> Find()
        {
            var users = await _context.Users
                .Where(u => u.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToListAsync();
            
            return View(users);
        }

        //listing all private chats in which you already registered
        public async Task<IActionResult> GetPrivateChats()
        {
            var privateChatsInDb = await _context.Chats
                .Include(model => model.Users)
                    .ThenInclude(model => model.User)
                .Where(model => model.ChatType == ChatType.Private
                        && model.Users
                            .Any(user => user.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToListAsync();
            return View("PrivateChatList", privateChatsInDb);
        }

        //listing all publics chats
        public async Task<IActionResult> GetPublicChats()
        {
            var privateChatsInDb = await _context.Chats
                .Include(model => model.Users)
                .Where(model => model.ChatType == ChatType.Group
                    && model.Users
                            .Any(user => user.UserId != User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToListAsync();
            return View("PublicChatList", privateChatsInDb);
        }

    }
}