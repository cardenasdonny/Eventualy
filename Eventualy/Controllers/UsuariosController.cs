using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Usuarios;
using Eventualy.Model.Entities.Usuarios;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(IUsuarioService usuarioService, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _usuarioService = usuarioService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Usuarios";

            return View(await _usuarioService.ObtenerListaUsuarios());
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioDto usuarioDto)
        {
            if (ModelState.IsValid) { 
                //comprobamos si existe o no el usuario
                var email = await _usuarioService.ObtenerUsuarioDtoPorEmail(usuarioDto.Email);
                if (email == null)
                {
                    try
                    {
                        var usuarioId = await _usuarioService.CrearUsuario(usuarioDto);
                        if (usuarioId == null)
                            return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });
                        if(usuarioId.Equals("ErrorPassword"))
                            return Json(new { isValid = false, tipoError = "danger", error = "Error el password debe cumplir con las políticas" });

                        return Json(new { isValid = true, operacion = "crear" });
                    }
                    catch (Exception)
                    {
                        return Json(new { isValid = false, tipoError = "danger", error = "Error al crear el usuario" });
                    }
                }
            }

            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", usuarioDto) });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RecordarMe, false);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("DashBoard", "Admin");
                    //return View();
                }
            }

            return View();
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(string id)
        {
            if(id==null)
                return Json(new { isValid = false, error = "No se encuentra el registro" });
            var usuario = await _userManager.FindByIdAsync(id);
            ViewBag.ListaRoles = new SelectList(await _roleManager.Roles.ToListAsync(),"Name","Name");
            var rolesUsuario = await _userManager.GetRolesAsync(usuario);
            UsuarioAsignarRol usuarioAsignarRol = new()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Rol = rolesUsuario.FirstOrDefault()
            };

            return View(usuarioAsignarRol);

        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Editar(string id, UsuarioAsignarRol usuarioAsignarRol)
        {
            if(id==null)
                return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = await _userManager.FindByIdAsync(usuarioAsignarRol.Id);
                    var resultado = await _userManager.AddToRoleAsync(usuario, usuarioAsignarRol.Rol);
                    if(resultado.Succeeded)
                        return Json(new { isValid = true, operacion = "editar" });
                    else
                        return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "danger", error = "Error al actualizar el registro" });
                }
            }

            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", usuarioAsignarRol) });

        }


    }
}
