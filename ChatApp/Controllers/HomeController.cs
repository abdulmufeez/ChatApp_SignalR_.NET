using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers{
    public class    HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}