using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> repo;

        public ProductService(ApplicationDbContext context, IGenericRepository<Product> repo)
        {
            this.repo = repo;
        }

        public T GetByIdGeneric<T>(string id)
            => this.repo.GetAllNotDeleted()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefault();
    }
}
