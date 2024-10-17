using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlace _home;
        private readonly IReview _review;
        private readonly ILogger<HomeController> _logger;

        // Combine both dependencies into one constructor
        public HomeController(IPlace home, IReview review, ILogger<HomeController> logger)
        {
            _home = home;
            _review = review;
            _logger = logger;
        }

        

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {

			List<Place> p = _home.GetAll();
			return View(p);
		}
        
        public IActionResult GetPlace(int id)
        {
            PlaceViewModel p = new PlaceViewModel();
            p = _review.Getinfo(id);
            return View(p);
        }
        public IActionResult Museums() {
          
            return PartialView("Details",_home.GetAllMuseum());
        }
        public IActionResult Hotels() {
          
            return PartialView("Details", _home.GetAllHotels());
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetUserId(int PlaceId ,string Review) 
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return RedirectToAction("AddReview", new { id = id, placeid=PlaceId ,review = Review });
        }
        public IActionResult AddReview(string id, int PlaceId, string Review)
        {
            if (string.IsNullOrEmpty(Review))
            {
                ViewBag.ErrorMessage = "Review cannot be empty.";
                return View(_review);
            }
            string user_id = id;
            _review.Add(user_id,PlaceId,Review);
            return RedirectToAction("GetReviews", new {PlaceId=PlaceId , id=id});
        }
        public IActionResult GetReviews(string id ,int PlaceId)
        {
            var p = _review.Getinfo(PlaceId);
            return View("GetPlace", p);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
