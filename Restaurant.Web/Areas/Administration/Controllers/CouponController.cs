using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Web.InputModel;
using Restaurant.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Areas.Administration.Controllers
{
    public class CouponController : BaseAdminController
    {
        private readonly IGenericRepository<Coupon> repo;

        public CouponController(IGenericRepository<Coupon> repo)
        {
            this.repo = repo;
        }

        public IActionResult All()
        {
            var vm = this.repo.GetAllMappedNotDeleted<CouponViewModel>()
                .ToList();
            return this.View(vm);
        }

        public IActionResult Edit(string id)
        {
            var item = this.repo.GetAllMappedNotDeleted<CouponInputModel>()
                .FirstOrDefault(x => x.Id == id);

            return this.View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CouponInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var product = this.repo.GetById(id);

            product.Name = input.Name;
            product.Discount = input.Discount;
            await this.repo.EditAsync(product);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.repo.RemoveAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Create()
        {
            var input = new CouponInputModel();

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var product = new Coupon
            {
                Name = input.Name,
                Discount = input.Discount
            };

            await this.repo.AddAsync(product);
            return this.RedirectToAction(nameof(this.All));
        }


    }
}
