using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;  // Assuming you're using Entity Framework
using System.Linq;
using WebApplication7.Repositry.IRepositry;

public class SearchController : Controller
{
    private readonly DepiContext _context; // DbContext to interact with the database
    IPlace home;
    public SearchController(DepiContext context,IPlace tt)
    {
        _context = context;
        home = tt;
    }

    [HttpGet]
    public IActionResult Index(string searchQuery)
    {
        if (string.IsNullOrEmpty(searchQuery))
        {
            return View(); // Return the default view if no search query is provided
        }

        // Query the database based on the search input
        Place results = new Place();
        results = _context.Places.FirstOrDefault(x => x.Place_Name == searchQuery);
        return View(results); // Pass the results to the view
    }
}
