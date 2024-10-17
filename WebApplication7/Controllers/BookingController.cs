using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using System.Linq;

public class BookingController : Controller
{
    private readonly DepiContext dbContext;

    public BookingController(DepiContext context)
    {
        dbContext = context;
    }

    [HttpGet]
    public IActionResult Create(int id)
    {
        BookingViewModel bookingViewModel = new BookingViewModel();
        Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

        if (place == null)
        {
            return NotFound("Place not found");
        }
       
        bookingViewModel.PlaceID = place.Place_Id;
        bookingViewModel.PlaceName = place.Place_Name;
        bookingViewModel.dbimage = place.dbimage;
        bookingViewModel.Description = place.Description;
        bookingViewModel.TotalAmount = place.Place_Price;
        bookingViewModel.PlaceType = place.Place_Type;

        return View(bookingViewModel);
    }

    [HttpPost]
    public IActionResult Create(BookingViewModel bookingViewModel,string promotioncode)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Payment", new { id = bookingViewModel.PlaceID, totalAmount = bookingViewModel.TotalAmount });
        }

        return View(bookingViewModel);
    }

    [HttpGet]
    public IActionResult Payment(int id, int numberofguests, string PromotionCode)
    {
        PaymentViewModel pp = new PaymentViewModel();
        Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

        if (place == null)
        {
            return NotFound("Place not found");
        }

        pp.TotalAmount = place.Place_Price * numberofguests;
        pp.PaymentCode = GeneratePaymentCode();
        pp.NumberOfGuests = numberofguests;

        if (string.IsNullOrEmpty(PromotionCode))
        {
            pp.TotalAmountAfterDiss = pp.TotalAmount;
            return View(pp);
        }
        if (!string.IsNullOrEmpty(PromotionCode))
        {
            var promo = dbContext.Promotions.FirstOrDefault(x => x.promotion_Code == PromotionCode);

            if (promo != null)
            {
                pp.TotalAmountAfterDiss = (place.Place_Price - (promo.Discount_Amount * place.Place_Price) / 100) * numberofguests;
            }
            else
            {
                // Add ModelState error if the promotion code is invalid
                ModelState.AddModelError("PromotionCode", "Invalid promotion code.");
                pp.TotalAmountAfterDiss = pp.TotalAmount; // No discount applied
            }
        }
        else
        {
            pp.TotalAmountAfterDiss = pp.TotalAmount; // No discount applied
        }

       
        // Check if the ModelState is valid
        if (!ModelState.IsValid)
        {
            BookingViewModel bookingViewModel = new BookingViewModel();
           

            if (place == null)
            {
                return NotFound("Place not found");
            }

            bookingViewModel.PlaceID = place.Place_Id;
            bookingViewModel.PlaceName = place.Place_Name;
            bookingViewModel.dbimage = place.dbimage;
            bookingViewModel.Description = place.Description;
            bookingViewModel.TotalAmount = place.Place_Price;
            bookingViewModel.PlaceType = place.Place_Type;

            return View("Create",bookingViewModel);
        }

        return View(pp);
    }

    [HttpPost]
    public IActionResult PaymentConfirmed(int TotalAmountAfterDiss)
    {
        return View("Success", TotalAmountAfterDiss);
    }

    private string GeneratePaymentCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
}
