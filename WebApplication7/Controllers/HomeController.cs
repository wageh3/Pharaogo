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
        private readonly IUser _user;
        private readonly ILogger<HomeController> _logger;
        
        DepiContext context;
        // Combine both dependencies into one constructor
        public HomeController(IPlace home, IReview review, ILogger<HomeController> logger, DepiContext _context, IUser user)
        {
            _home = home;
            _review = review;
            _logger = logger;
            context = _context;
            _user = user;
        }
        public IActionResult Index()
        {

			var p = _home.GetAll();
			return View(p);
		}
        public IActionResult Header_Museums()
        {

            return PartialView("Places", _home.GetAllMuseum());
        }public IActionResult Header_Hotels()
        {

            return PartialView("Places", _home.GetAllHotels());
        }

        public IActionResult GetPlace(int id)
        {
            PlaceViewModel p = new PlaceViewModel();
            p = _review.Getinfo(id);
            return View(p);
        }
        public IActionResult Museums() {
          
            return PartialView("Details", _home.GetAllMuseum());
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
            DepiContext context = new DepiContext();
            var obj=_user.GetUser(id);
            string UserName="UnKnown";
            if (obj != null)
            {

                UserName = obj;
               
            }
            string user_id = id;
            _review.Add(user_id, PlaceId, Review,UserName);
            
                return RedirectToAction("GetReviews", new { username = UserName, PlaceId = PlaceId, id = id });
            
        }
        public IActionResult GetReviews(string username ,string id, int PlaceId)
        {
            var p = _review.Getinfo(PlaceId);
            var temp = p.review.FirstOrDefault(x => x.UserName == username);
            ViewBag.UserNamee = username;
            return View("GetPlace", p);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UpdateRate(int id, int rating)

        {
            Place p = _home.GetById(id);
            decimal temp;
            if (p == null)
            {
                
                return NotFound();
            }
            p.cnt = p.cnt + 1;
            p.SumOfRates = p.SumOfRates + rating;
            context.SaveChanges();
            temp = p.SumOfRates / p.cnt;
            p.Place_Rating = temp.ToString();
            context.SaveChanges();
            PlaceViewModel pp = new PlaceViewModel();
            pp = _review.Getinfo(id);
            return View("GetPlace",pp);
        }
    }
}
