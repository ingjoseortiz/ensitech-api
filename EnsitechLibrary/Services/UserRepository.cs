using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Services
{
    public class UserRepository : IUserRepository
    {
        public string Login(object o)
        {
            throw new NotImplementedException();
        }

        public IActionResult LogOut()
        {
            throw new NotImplementedException();
        }
    }
}