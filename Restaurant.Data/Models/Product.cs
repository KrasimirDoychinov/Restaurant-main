using Restaurant.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Data.Models
{
    [Table(name: "Products", Schema = "17118018")]
    public class Product : BaseDeletableModel
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

        public string ImagePath { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public string Ingredients { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; } = new List<ProductOrder>();
    }
}
