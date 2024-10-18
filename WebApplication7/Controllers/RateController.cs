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
        
        public void Index(int id,int rating)

        {
            Place p = place.GetById(id);
            decimal temp;
            p.cnt = p.cnt + 1;
            p.SumOfRates=p.SumOfRates+ rating;
            context.SaveChanges();
            temp=p.SumOfRates/p.cnt;
            p.Place_Rating = temp.ToString();
            context.SaveChanges();
           
        }
    }
}
