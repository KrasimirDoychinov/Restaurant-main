using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductOrder> productOrderRepo;
        private readonly IGenericRepository<Order> orderRepo;
        private readonly IOrderService orderService;
        private readonly ICouponService couponService;
        private readonly UserManager<User> userManager;
        public OrderController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductOrder> productOrderRepo,
            IGenericRepository<Order> orderRepo,
            IOrderService orderService,
            ICouponService couponService,
            UserManager<User> userManager)
        {
            this.productRepo = productRepo;
            this.productOrderRepo = productOrderRepo;
            this.orderRepo = orderRepo;
            this.orderService = orderService;
            this.couponService = couponService;
            this.userManager = userManager;
        }

        public string _UserId { get { return this.userManager.GetUserAsync(this.User).Result.Id; } }

        public IActionResult All(double? discountedTotalPrice)
        {
            var items = this.orderService.GetAllOrdersByUser<OrderViewModel>(this._UserId);
            items.Products = items.Products.Where(x => !x.Product.IsDeleted);
            if (discountedTotalPrice.HasValue)
            {
                items.TotalPrice = discountedTotalPrice.Value;
            }
            return this.View(items);
        }

        [HttpPost]
        public async Task<IActionResult> All(string id, string couponName)
        {
            var coupon = this.couponService.GetCoupon(couponName);
            var order = this.orderRepo.GetById(id);
            if (coupon == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            order.TotalPrice = await this.couponService.ApplyCoupon(coupon, order); 
            return this.RedirectToAction(nameof(this.All), new { discountedTotalPrice = order.TotalPrice });
         }

        public async Task<IActionResult> CreateOrder(string productId)
        {
            var product = this.productRepo.GetById(productId);
            var newProductOrder = new ProductOrder();
            if (this.orderService.TryGetOrderByUserId(this._UserId, out Order order))
            {
                order.TotalPrice += product.Price;
                newProductOrder = new ProductOrder
                {
                    ProductId = productId,
                    OrderId = order.Id
                };
                await this.orderRepo.EditAsync(order);
            }
            else
            {
                order = new Order
                {
                    UserId = this._UserId,
                    TotalPrice = product.Price
                };

                newProductOrder = new ProductOrder
                {
                    ProductId = productId,
                    OrderId = order.Id
                };

                await this.orderRepo.AddAsync(order);
            }

            await this.productOrderRepo.AddAsync(newProductOrder);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Finish(string id)
        {
            var order = this.orderRepo.GetById(id);
            order.IsFinished = true;
            await this.orderRepo.EditAsync(order);

            return this.Redirect("/");
        }
    }
}
