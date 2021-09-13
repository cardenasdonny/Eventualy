using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Usuarios;
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

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            ViewBag.Titulo = "Usuarios";
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioDto usuarioDto)
        {
            //comprobamos si existe o no el usuario
            var email = await _usuarioService.ObtenerUsuarioDtoPorEmail(usuarioDto.Email);
            if (email == null)
            {
                try
                {
                    var usuarioId = await _usuarioService.CrearUsuario(usuarioDto);
                    if (usuarioId != null)
                    {

                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }


            return View();
        }
    }
}
