using System.ComponentModel.DataAnnotations;

namespace Summit_Task.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImageUrl { get; set; }
        public int? CategoryId { get; set; }
    }
}
