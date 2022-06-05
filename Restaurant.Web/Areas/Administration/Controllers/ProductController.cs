using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Web.InputModel;
using Restaurant.Web.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : BaseAdminController
    {
        private readonly IGenericRepository<Product> repo;
        private readonly IProductService productService;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IGenericRepository<Product> repo,
            IProductService productService,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.repo = repo;
            this.productService = productService;
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult All()
        {
            var items = this.repo.GetAllMappedNotDeleted<ProductViewModel>();
            items.AsParallel().ForAll(x => x.Orders = x.Orders.Where(x => !x.Order.IsDeleted && !x.Order.IsFinished).ToList());
            return this.View(items);
        }

        public IActionResult Create()
        {
            var input = new ProductInputModel();

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var product = new Product
            {
                Name = input.Name,
                Ingredients = input.Ingredients,
                Price = input.Price,
                ProductType = input.ProductType,
                Weight = input.Weight
            };
             
            await this.UpdateImagePath(this.webHostEnvironment.WebRootPath, product, input.Image);
            await this.repo.AddAsync(product);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.repo.RemoveAsync(id);
            return this.RedirectToAction(nameof(All));
        }
          
        public IActionResult Edit(string id)
        {
            var item = this.productService.GetByIdGeneric<ProductEditModel>(id);
            return this.View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductEditModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var product = this.repo.GetById(id);

            product.Name = input.Name;
            product.Ingredients = input.Ingredients;
            product.Price = input.Price;
            product.Weight = input.Weight;
            product.ProductType = input.ProductType;
            await this.repo.EditAsync(product);
            await this.UpdateImagePath(this.webHostEnvironment.WebRootPath, product, input.Image);
            return this.RedirectToAction(nameof(this.All));
        }

        private async Task UpdateImagePath(string webRootPath, Product product, IFormFile image)
        {
            if (image == null)
            {
                return;
            }
            else
            {  
                product.ImagePath = $"{product.Id}(Product).jpeg";

                using (var fs = new FileStream(
                   $"{webRootPath}/Images/{product.ImagePath}", FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }

                
                await this.context.SaveChangesAsync();
            }
        }

    }
}
