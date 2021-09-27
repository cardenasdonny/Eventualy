using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Administrador, Usuario")]
        public IActionResult Dashboard()
        {
            
            return View();
        }
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }
}
