using Restaurant.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public OrderByEnum OrderBy { get; set; }

        [Display(Name = "Descending")]
        public bool IsDescending { get; set; } = false;

        [Display(Name = "Search by name")]
        public string Search { get; set; }
    }
}
