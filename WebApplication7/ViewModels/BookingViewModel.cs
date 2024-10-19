using System.ComponentModel.DataAnnotations;

namespace WebApplication7.ViewModels
{
    public class BookingViewModel
    {
        public int? dayes { get; set; }

        public int PlaceID { get; set; }  // Place ID (can be optional)
        public string PlaceType { get; set; } 
        public string PlaceName { get; set; } = "Default Place";  // Default place name if no place is selected
        public byte[]? dbimage { get; set; }
        public string? imageSrc
        {
            get
            {
                if (dbimage != null)
                {
                    string base64String = Convert.ToBase64String(dbimage, 0, dbimage.Length);
                    return "data:image/jpg;base64," + base64String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Description { get; set; } = "No specific place selected. Please select a place.";

        [Required]
        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; } = 1;

        [Display(Name = "Promotion Code")]
        public string? PromotionCode { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; } // Correct property name
        public int BookingId { get; set; } // This will hold the booking ID
    }
}





