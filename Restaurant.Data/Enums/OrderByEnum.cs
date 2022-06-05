using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Enums
{
    public enum OrderByEnum
    {
        None = 0,
        Price = 4,
        Ingredients = 1,
        [Display(Name = "Created on")]
        CreatedOn = 2,
    }
}
