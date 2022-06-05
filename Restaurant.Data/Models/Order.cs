using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models
{
    [Table(name: "Orders", Schema = "17118018")]
    public class Order : BaseDeletableModel
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public bool IsFinished { get; set; } = false;

        public bool IsCouponUsed { get; set; } = false;

        public virtual ICollection<ProductOrder> Products { get; set; } = new List<ProductOrder>();
    }
}
