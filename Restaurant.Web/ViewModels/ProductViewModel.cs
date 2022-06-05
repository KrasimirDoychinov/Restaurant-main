using Restaurant.Data.Enums;
using Restaurant.Data.Models;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.ViewModels
{
    public class ProductViewModel : BaseViewModel, IMapFrom<Product>
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double Weight { get; set; }

        public string ImagePath { get; set; }

        public ProductType ProductType { get; set; }

        public string Ingredients { get; set; }

        public ICollection<ProductOrderViewModel> Orders { get; set; } 
    }
}
