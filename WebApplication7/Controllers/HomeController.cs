using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
		IPlace home;
		public HomeController(IPlace tt)
		{
			home = tt;
		}

		private readonly ILogger<HomeController> _logger;

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {

			List<Place> p = home.GetAll();
			return View(p);
		}
        public IActionResult GetPlace(int id)
        {
            Place p = new Place();
            p = home.Get(id);
            return View(p);
        }
        public IActionResult Museums() {
          
            return View("Index",home.GetAllMuseum());
        }
        public IActionResult Hotels() {
          
            return View("Index", home.GetAllHotels());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
