using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Web.Areas.Administration.Controllers
{
    public class AdminController : BaseAdminController
    {
        public AdminController()
        {

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Panel()
        {
            return this.View();
        }
    }
}
