﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Contracts
{
    public interface IUserService
    {
        string GetUserNameById(string id);
    }
}
