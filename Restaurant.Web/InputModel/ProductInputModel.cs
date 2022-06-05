using Microsoft.AspNetCore.Http;
using Restaurant.Data.Enums;
using Restaurant.Web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Web.InputModel
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(0, 50)]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000)]
        public double Weight { get; set; }

        [Required]
        [ImageValidator]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Product type")]
        public ProductType ProductType { get; set; }
        
        [Required]
        public string Ingredients { get; set; }

    }
}
