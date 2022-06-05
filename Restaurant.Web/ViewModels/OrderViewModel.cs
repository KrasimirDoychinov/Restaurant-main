using Restaurant.Data.Models;
using Restaurant.Services.Mapper;
using System.Collections.Generic;

namespace Restaurant.Web.ViewModels
{
    public class OrderViewModel : BaseViewModel, IMapFrom<Order>
    {
        public string UserId { get; set; }
        
        public string UserName { get; set; }

        public double Price { get; set; }

        public bool IsFinished { get; set; }

        public bool IsCouponUsed { get; set; }

        public IEnumerable<ProductOrderViewModel> Products { get; set; }

        public double TotalPrice { get; set; }

        public string CouponName { get; set; }
    }
}
