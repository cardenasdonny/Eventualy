using Eventualy.Business.Dtos.Roles;
using Eventualy.Model.Entities.Usuarios;
using Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Roles";
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(CrearRolDto crearRolDto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _roleManager.CreateAsync(new IdentityRole(crearRolDto.Rol));
                if(resultado.Succeeded)
                    return Json(new { isValid = true, operacion = "crear" });
                return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });

            }

            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", crearRolDto) });

        }

    }
}
