using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Promotion
    {
        [Key]
        public int promotion_Id { get; set; }
        [Display(Name = "Promotion Code :")]
        [Required]

        public string promotion_Code { get;set; }
        [Display(Name = "Discount Amount :")]
        public int Discount_Amount {  get; set; }
        
       
    }
}
