using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Controllers
{
    public class RateController : Controller
    {
        IPlace place;
        DepiContext context;
        public RateController(IPlace temp,DepiContext _context)
        {
            place= temp;
            context= _context;
        }
        
       
    }
}
