using System.ComponentModel.DataAnnotations;

namespace TaskTwo.DTOs
{
    public class AddOrEditProductDto
    {
        [Required(ErrorMessage = "enter your name ...!!")]
        [MinLength(5, ErrorMessage = "the min length 5.......!!")]
        [MaxLength(30, ErrorMessage = "the min length 5.......!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "enter your price...!!")]
        [Range(20, 3000)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "enter your Description ...!!")]
        [MinLength(10)]
        public string Description { get; set; }
    }
}
