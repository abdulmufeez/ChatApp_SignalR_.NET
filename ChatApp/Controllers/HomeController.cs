using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers{    
    [Authorize]
    public class HomeController : Controller
    {
         private readonly AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;
                
        public async Task<IActionResult> Index() 
        {
            var chatsInDb = await _context.Chats
                .Include(x => x.Users)
                .Where(model => model.Users
                    .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .ToListAsync();            
            
            return View(chatsInDb);
        }
    }
}