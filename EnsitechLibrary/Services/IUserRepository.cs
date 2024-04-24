using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Models
{
    public interface IUserRepository
    {
        string Login(Object o);
        IActionResult LogOut(); 
    }
}