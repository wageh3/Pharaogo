using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Contact_Us
    {
        [Key]
        public string Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("user")]
        public string User_ID { get; set; }
        public User user { get; set; }

    }
}
