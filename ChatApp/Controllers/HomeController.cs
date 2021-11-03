using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}