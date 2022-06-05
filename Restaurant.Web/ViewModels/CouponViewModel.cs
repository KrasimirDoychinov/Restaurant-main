using Restaurant.Data.Models;
using Restaurant.Services.Mapper;

namespace Restaurant.Web.ViewModels
{
    public class CouponViewModel : BaseViewModel, IMapFrom<Coupon>
    {
        public string Name { get; set; }

        public double Discount { get; set; }

        public bool IsUsed { get; set; }
    }
}
