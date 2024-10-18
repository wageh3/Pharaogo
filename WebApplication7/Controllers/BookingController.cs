using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
    // Check if the user is authenticated
    if (!User.Identity.IsAuthenticated)
    {
        TempData["ErrorMessage"] = "You are not authorized to access this page.Please Login First";
        return RedirectToAction("Login", "Account"); // Adjust this to your login action
    }

    PaymentViewModel pp = new PaymentViewModel();
    Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);

    // Check if the place is null
    if (place == null)
    {
        return NotFound("Place not found");
    }

    // Set the base payment values
    pp.TotalAmount = place.Place_Price * numberofguests;
    pp.PaymentCode = GeneratePaymentCode();
    pp.NumberOfGuests = numberofguests;
    pp.TotalAmountAfterDiss = pp.TotalAmount; // Initialize with no discount

    // If a promotion code is provided
    if (!string.IsNullOrEmpty(PromotionCode))
    {
        var promo = dbContext.Promotions.FirstOrDefault(x => x.promotion_Code == PromotionCode);

        if (promo != null)
        {
            // Calculate discount
            pp.TotalAmountAfterDiss = (place.Place_Price - (promo.Discount_Amount * place.Place_Price) / 100) * numberofguests;
        }
        else
        {
            // Add an error for an invalid promotion code
            ModelState.AddModelError("PromotionCode", "Invalid promotion code.");
        }
    }

    // If the model state is invalid, return to a different view (like booking form)
    if (!ModelState.IsValid)
    {
        BookingViewModel bookingViewModel = new BookingViewModel
        {
            PlaceID = place.Place_Id,
            PlaceName = place.Place_Name,
            dbimage = place.dbimage,
            Description = place.Description,
            TotalAmount = place.Place_Price,
            PlaceType = place.Place_Type
        };

        return View("Create", bookingViewModel);
    }

    // Return the payment view with the PaymentViewModel
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
