using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Enums
{
    public enum ProductType
    {
        [Display(Name = "Main dish")]
        MainDish = 0,
        [Display(Name = "Side dish")]
        SideDish = 1,
        [Display(Name = "Salad")]
        Salad = 2,
        [Display(Name = "Desert")]
        Desert = 3,
    }
}
