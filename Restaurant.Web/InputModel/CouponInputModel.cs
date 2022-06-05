using Restaurant.Data.Models;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.InputModel
{
    public class CouponInputModel : IMapFrom<Coupon>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(0, 50)]
        public double Discount { get; set; }

        public bool IsUsed { get; set; }
    }
}
