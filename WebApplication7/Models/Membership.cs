using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Membership
    {
        [Key]
        public int Membership_Id { get; set; }
        [ForeignKey("user")]
        [Required]
        public string User_ID { get; set; }
        public User user { get; set; }
        [ForeignKey("promotion")]
        [Required]
        public int promotion_ID { get; set; }
        public Promotion promotion { get; set; }

    }
}
