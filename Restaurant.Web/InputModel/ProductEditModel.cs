using Restaurant.Data.Models;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.InputModel
{
    public class ProductEditModel : ProductInputModel, IMapFrom<Product>
    {
    }
}
