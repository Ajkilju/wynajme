using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Data;
using Microsoft.EntityFrameworkCore;


namespace Wynajme_AspNetCore_v2.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Index(string UserId)
        {
            return View();
        }
    }
}