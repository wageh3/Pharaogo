using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class WishList
    {
        [ForeignKey("place")]
        public int PlaceId { get; set; }
        public Place place { get; set; }
        [ForeignKey("user")]
        public int UserID { get; set; }
        public User user { get; set; }
    }
}
