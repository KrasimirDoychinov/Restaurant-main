using Restaurant.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Contracts
{
    public interface IOrderService
    {
        bool TryGetOrderByUserId(string userId, out Order order);

        int? GetUserOrderCount(string userId);

        T GetAllOrdersByUser<T>(string userId);
    }
}
