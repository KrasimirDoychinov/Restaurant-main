using Restaurant.Data;
using Restaurant.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string GetUserNameById(string id)
            => this.context.Users
            .FirstOrDefault(x => x.Id == id)
            .UserName;
    }
}
