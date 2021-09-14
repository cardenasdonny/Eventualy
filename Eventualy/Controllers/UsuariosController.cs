using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Usuarios;
using Eventualy.Model.Entities.Usuarios;
using Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsuariosController(IUsuarioService usuarioService, SignInManager<Usuario> signInManager)
        {
            _usuarioService = usuarioService;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Usuarios";

            return View(await _usuarioService.ObtenerListaUsuarios());
        }
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

    }
}
