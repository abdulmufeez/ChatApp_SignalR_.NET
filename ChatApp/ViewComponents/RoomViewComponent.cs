using System.Linq;
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
            var chatsInDb = await _context.Chats.ToListAsync();
            return View(chatsInDb);
        }
    }
}