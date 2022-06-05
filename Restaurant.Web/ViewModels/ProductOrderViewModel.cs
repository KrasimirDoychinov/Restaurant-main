using Restaurant.Data.Models;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.ViewModels
{
    public class ProductOrderViewModel : BaseViewModel, IMapFrom<ProductOrder>
    {
        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public string OrderId { get; set; }

        public OrderViewModel Order { get; set; } 
    }
}
