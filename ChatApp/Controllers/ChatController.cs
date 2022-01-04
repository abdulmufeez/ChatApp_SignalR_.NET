using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using ChatApp.UtilityClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        //User.FindFirst(ClaimTypes.NameIdentifier).Value
        //basically this line of code get the name of the current signedin user
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context) => _context = context;
        
        public IActionResult Index() => View(); 

        // public async Task<IActionResult> Index() 
        // {
        //     var chatsInDb = await _context.Chats
        //         .Include(x => x.Users)
        //         .Where(model => !model.Users
        //             .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //         .ToListAsync();            
            
        //     return View(chatsInDb);
        // }                

        [HttpPost]
        public async Task<IActionResult> CreateGroup(string name)
        {
            var chat = new Models.Chat{
                Name = name,
                ChatType = ChatType.Group
            };

            chat.Users.Add(new ChatUser {
                UserId = GetUserId(),       //i don't what is happening here 
                                                                                //i think it basically take out the id of user crated when registration
                Role = UserRole.Admin
            });

            await _context.Chats.AddAsync(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrivateGroup(string userId)
        {
            var chat = new Models.Chat{                
                ChatType = ChatType.Private
            };

            chat.Users.Add(new ChatUser{
                UserId = userId,
                Role = UserRole.Member              
            });

            chat.Users.Add(new ChatUser{
                UserId = GetUserId(),
                Role = UserRole.Admin                
            });

            await _context.Chats.AddAsync(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("GetChat","Home", new { id = chat.Id });        //after calling savechanges function 
                                                                                    //_context will automatically assigned chatId to specific chat id
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

        [HttpPost]
        public async Task<IActionResult> JoinGroup(int id)
        {
             var chatUser = new ChatUser {
                ChatId = id,
                UserId = GetUserId(),
                Role = UserRole.Member
            };

            await _context.ChatUsers.AddAsync(chatUser);

            await _context.SaveChangesAsync();

            return RedirectToAction("GetChat", new {id = id});
        }
    }    
}