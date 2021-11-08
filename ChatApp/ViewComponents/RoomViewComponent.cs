using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.ViewComponents{
    public class RoomViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public RoomViewComponent(AppDbContext context) => _context = context;
        
        public async Task<IViewComponentResult> InvokeAsync(){
            var chatUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var chatsInDb = await _context.ChatUsers
            .Include(chatuser => chatuser.Chat)
            .Where(chatuser => chatuser.UserId == chatUser)
            .Select(chatuser => chatuser.Chat)
            .ToListAsync();
            return View(chatsInDb);
        }
    }
}