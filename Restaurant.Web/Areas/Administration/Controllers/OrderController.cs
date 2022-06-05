using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Areas.Administration.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IGenericRepository<Order> orderRepo;
        private readonly IUserService userService;

        public OrderController(IGenericRepository<Order> orderRepo,
            IUserService userService)
        {
            this.orderRepo = orderRepo;
            this.userService = userService;
        }

        public IActionResult All()
        {
            var items = this.orderRepo.GetAllMappedNotDeleted<OrderViewModel>()
                .ToList();
            foreach (var item in items)
            {
                item.UserName = this.userService.GetUserNameById(item.UserId);
                item.Products = item.Products.Where(x => !x.Product.IsDeleted);
            }
            return this.View(items);
        }


    }
}
