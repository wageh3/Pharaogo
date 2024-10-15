using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
