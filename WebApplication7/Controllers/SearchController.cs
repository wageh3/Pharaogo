using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;  // Assuming you're using Entity Framework
using System.Linq;
using WebApplication7.Repositry.IRepositry;
using Microsoft.IdentityModel.Tokens;

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
         
        var results = _context.Places.Where(x => x.Place_Name.Contains(searchQuery)).ToList();
        if (string.IsNullOrEmpty(searchQuery) || results == null || results.IsNullOrEmpty())
        {
            return View("NotFound");
        }
        return View(results); 
    }
}
