using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Usuarios;
using Eventualy.Model.Entities.Usuarios;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuarioService usuarioService, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
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

        public IActionResult OlvidePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OlvidePassword(RecuperarPasswordDto recuperarPasswordDto)
        {
            if (ModelState.IsValid)
            {
                //buscar el usuario a traves del correo
                var usuario = await _userManager.FindByEmailAsync(recuperarPasswordDto.Email);
                if (usuario != null)
                {
                    //generaramos un token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    //Creamos un link para resetear el password
                    var passwordResetLink = Url.Action("ResetearPassword", "Usuarios", new { email = recuperarPasswordDto.Email, token = token }, Request.Scheme);

                    //Envio de email a traves de API (sendgrid)

                    //configuramos el cliente
                    var clienteCorreo = new SendGridClient(_configuration["Email:Key"]);

                    //configuramos el mensaje

                    var mensaje = new SendGridMessage
                    {
                        From = new EmailAddress(_configuration["Email:FromEmail"], _configuration["Email:FromName"]),
                        Subject = "Eventuality - Recuperar contraseña",
                        PlainTextContent = passwordResetLink,
                        HtmlContent = "<h1>Haga clic en el link para recuperar la contraseña:<h1><br><br>" + passwordResetLink
                    };
                    mensaje.AddTo(recuperarPasswordDto.Email);
                    mensaje.SetClickTracking(false, false);
                    var resultado = await clienteCorreo.SendEmailAsync(mensaje);

                    if (resultado.IsSuccessStatusCode)
                        return RedirectToAction("Login");



                    /*
                    //enviamos el correo por SMTP
                    MailMessage mensaje = new();
                    mensaje.To.Add(recuperarPasswordDto.Email); //destinarario
                    mensaje.Subject = "Eventuality - recuperar password"; // Asunto
                    mensaje.Body = passwordResetLink; //cuerpo del correo
                    mensaje.IsBodyHtml = false;
                    mensaje.From = new MailAddress("pruebas@xofsystems.com", "Eventuality notificaciones");

                    // configurar el servidor smtp
                    SmtpClient smtpClient = new("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("pruebas@xofsystems.com", "Tempo123!");

                    try
                    {
                        smtpClient.Send(mensaje);
                        return RedirectToAction("Login");

                    }
                    catch (Exception)
                    {

                        throw;
                    }  
                    */

                }
            }

            return View(recuperarPasswordDto);
        }

        [HttpGet]
        public IActionResult ResetearPassword(string token, string email)
        {
            if(token == null || email == null)
            {
                ModelState.AddModelError("", "Error de token o de email");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetearPassword(ResetearPasswordDto resetearPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(resetearPasswordDto.Email);
                if (usuario != null)
                {
                    //reseteamos el password
                    var resultado = await _userManager.ResetPasswordAsync(usuario, resetearPasswordDto.Token, resetearPasswordDto.Password);
                    if (resultado.Succeeded)
                        return RedirectToAction("Login");
                }

            }
            return View(resetearPasswordDto);
        }

    }
}
