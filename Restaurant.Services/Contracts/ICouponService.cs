using Restaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Contracts
{
    public interface ICouponService
    {
        Coupon GetCoupon(string name);

        Task<double> ApplyCoupon(Coupon coupon, Order order);
    }
}
