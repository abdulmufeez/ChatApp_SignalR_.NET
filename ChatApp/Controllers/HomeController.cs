using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers{
    [AllowAnonymous]
    public class HomeController : Controller
    {
         private readonly AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;
        public async Task<IActionResult> Index() => View(await _context.Chats.ToListAsync());
    }
}