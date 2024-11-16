using System.ComponentModel.DataAnnotations;

namespace TaskTwo.DTOs
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
