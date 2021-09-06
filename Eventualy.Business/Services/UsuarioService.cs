using Eventualy.Business.Abstract;
using Eventualy.Model.Entities.Usuarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public async Task<string> CrearUsuario(UsuarioRegistrarDto UsuarioRegistrarDto)
        //{
        //    if (UsuarioRegistrarDto == null)
        //    {
        //        throw new ArgumentNullException(nameof(UsuarioRegistrarDto));
        //    }
        //    Usuario usuario = new()
        //    {
        //        UserName = UsuarioRegistrarDto.Email,
        //        Email = UsuarioRegistrarDto.Email,
        //        Nombres = UsuarioRegistrarDto.Nombres,
        //        Apellidos = UsuarioRegistrarDto.Apellidos,
        //        Estado = true
        //    };

        //    var resultado = await _userManager.CreateAsync(usuario, UsuarioRegistrarDto.Password);


        //    if (resultado.Errors.Any())
        //        return "ErrorPassword";

        //    if (resultado.Succeeded)
        //    {
        //        return usuario.Id;
        //    }
        //    return null;

        //}
    }
}
