using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using System.Linq;

public class BookingController : Controller
{

    DepiContext dbContext = new DepiContext();
    // GET: Booking/Create
    [HttpGet]
    public IActionResult Create(int Id)
    {
        BookingViewModel bookingViewModel = new BookingViewModel();

        // If placeId is provided, fetch the place details from the database
       
        
        Place place = dbContext.Places.FirstOrDefault(x=>x.Place_Id==Id);
          
        bookingViewModel.PlaceID = place.Place_Id;
        bookingViewModel.PlaceName = place.Place_Name;
        bookingViewModel.dbimage = place.dbimage; // Set actual image URL
        bookingViewModel.Description = place.Description;
        bookingViewModel.TotalAmount = place.Place_Price; // Set total amount based on the place price
       
        return View(bookingViewModel);
    }

    // POST: Booking/Create
    [HttpPost]
    public IActionResult Create(BookingViewModel bookingViewModel)
    {
        if (ModelState.IsValid)
        {
            // Here you can handle the booking logic (e.g., save to the database)
            // For now, we will redirect to a payment page
            return RedirectToAction("Payment", new { totalAmount = bookingViewModel.TotalAmount });  // Redirecting to Payment with total amount
        }

        return View(bookingViewModel);
    }

    // GET: Booking/Payment
    [HttpGet]
    public IActionResult Payment(int id,int numberofguests,string pormotioncode)
    {
        PaymentViewModel pp = new PaymentViewModel();
        Place place = dbContext.Places.FirstOrDefault(x => x.Place_Id == id);
        // Create a payment view model to pass to the payment view
        if (ModelState.IsValid) 
        { 
                if (pormotioncode!= null || pormotioncode=="") 
                {
                    var Result = dbContext.Promotions.FirstOrDefault(x=>x.promotion_Code==pormotioncode);
                    pp.TotalAmount = place.Place_Price*numberofguests;
                    pp.TotalAmountAfterDiss = ((place.Place_Price)-((Result.Discount_Amount*place.Place_Price)/100))*numberofguests;
                    pp.PaymentCode = GeneratePaymentCode();// Generate a fake payment code
                    pp.NumberOfGuests = numberofguests;
                }
        }

        return View("Payment",pp); // Pass the payment model to the Payment view
    }

    // POST: Booking/PaymentConfirmed
    [HttpPost]
    public IActionResult PaymentConfirmed(PaymentViewModel model)
    {
        // Simulate payment confirmation logic
        // Here you would normally handle the payment processing

        // Simulate saving the booking (you would normally get booking details from the model)
        // Assuming you have a way to identify the booking (e.g., from a hidden field)

        TempData["AlertMessage"] = $"Payment successful! Amount: {model.TotalAmount}, Payment Code: {model.PaymentCode}";

        return RedirectToAction("Success"); // Redirect to a success page
    }

    private string GeneratePaymentCode()
    {
        // This method generates a fake payment code
        return Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); // Example code
    }

    // Add your success method if you need one
    public IActionResult Success()
    {
        return View();
    }
}
