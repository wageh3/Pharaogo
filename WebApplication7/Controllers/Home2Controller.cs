using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
	public class Home2Controller : Controller
	{
		IPlace home;
		public Home2Controller(IPlace tt)
		{
			home = tt;
		}

		public IActionResult Index()
		{
			List<Place> p = home.GetAll();
			return View(p);
		}
		public IActionResult GetPlace(int id)
		{
			Place p = new Place();
			p = home.GetById(id);
			return View(p);
		}
	}
}
