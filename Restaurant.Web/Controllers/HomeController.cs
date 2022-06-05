using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Enums;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Services.Implementations;
using Restaurant.Web.Models;
using Restaurant.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Product> productRepo;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Product> productRepo)
        {
            this._logger = logger;
            this.productRepo = productRepo;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel();
            var filteredProducts = new List<ProductViewModel>();
            
            if (filteredProducts.Count() <= 0)
            {
                filteredProducts = this.productRepo.GetAllMappedNotDeleted<ProductViewModel>()
                .ToList();
            }

            vm.Products = filteredProducts;
            return View(vm);
        }

        public IActionResult MicrosoftAuth()
        {
            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
