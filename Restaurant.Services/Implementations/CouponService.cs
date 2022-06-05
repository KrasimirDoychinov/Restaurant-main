using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class CouponService : ICouponService
    {
        private readonly ApplicationDbContext context;

        public CouponService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Coupon GetCoupon(string name)
            => this.context.Coupons
            .FirstOrDefault(x => !x.IsUsed && x.Name.ToLower() == name.ToLower());

        public async Task<double> ApplyCoupon(Coupon coupon, Order order)
        { 
            coupon.IsUsed = true;
            order.TotalPrice -= coupon.Discount;
            order.IsCouponUsed = true;
            if (order.TotalPrice < 0)
            {
                order.TotalPrice = 0;
            }
            await this.context.SaveChangesAsync();
            return order.TotalPrice;
        }
    }
}
