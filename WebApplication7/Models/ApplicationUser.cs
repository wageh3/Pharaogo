using Microsoft.AspNetCore.Identity;
namespace WebApplication7.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
		public string ImageUrl { get; set; }

	}
}
