using Microsoft.AspNetCore.Mvc;

namespace ashishprac.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
