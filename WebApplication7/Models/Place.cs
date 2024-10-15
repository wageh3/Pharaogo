using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Place
    {
        [Key]
        public int Place_Id { get; set; }
        [Required]
        [Display(Name = "Place Name : ")]

        public string Place_Name { get; set; }
        [Required]
        [Display(Name = "Type :")]
        public string Place_Type { get; set; }
        [Display(Name = "City :")]
        public string Place_City { get; set; }
        [Required]
        [Display(Name = "Price :")]

        public int Place_Price { get; set; }
        [Display(Name = "Rating :")]

        public string Place_Rating { get; set; } = "unrated";
        public string? Description { get; set; }

        public string? Place_Photo { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]

        public IFormFile? clientFile { get; set; }

        public byte[]? dbimage { get; set; }

        [NotMapped]
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
    }
}
