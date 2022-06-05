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
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> repo;

        public OrderService(IGenericRepository<Order> repo)
        {
            this.repo = repo;
        }

        public bool TryGetOrderByUserId(string userId, out Order order)
        {
            order = this.repo.GetAllNotDeleted()
                .FirstOrDefault(x => !x.IsFinished && x.UserId == userId);

            return order == null ? false : true;
        }

        public T GetAllOrdersByUser<T>(string userId)
            => this.repo.GetAllNotDeleted()
            .Where(x => !x.IsFinished && x.UserId == userId)
            .To<T>()
            .FirstOrDefault();

        public int? GetUserOrderCount(string userId)
            => this.repo.GetAllNotDeleted()
            .FirstOrDefault(x => !x.IsFinished && x.UserId == userId)
            ?.Products
            ?.Where(x => !x.Product.IsDeleted)
            ?.Count();
    }
}
