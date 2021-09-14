using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Usuarios;
using Eventualy.Model.Entities.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<UsuarioResumenDto>> ObtenerListaUsuarios()
        {
            List<UsuarioResumenDto> listaUsuariosResumenDtos = new();
            var usuarios = await _userManager.Users.ToListAsync();
            usuarios.ForEach(u =>
            {
                UsuarioResumenDto usuarioResumenDto = new()
                {
                    UsuarioId = u.Id,
                    Correo = u.Email,
                    Estado = u.Estado ? "Habilitado" : "Deshabilitado",
                    Rol = "Sin rol"
                };
                listaUsuariosResumenDtos.Add(usuarioResumenDto);
            });
            return listaUsuariosResumenDtos;
        }

        public async Task<UsuarioResumenDto>ObtenerUsuarioDtoPorEmail(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario != null)
            {
                UsuarioResumenDto usuarioResumenDto = new()
                {
                    UsuarioId = usuario.Id,
                    Correo = usuario.Email,
                    Estado = usuario.Estado?"Habilitado":"Deshabilitado",
                    Rol = "N/A"
                };
                return usuarioResumenDto;
            }
            return null;
        } 


        public async Task<string> CrearUsuario(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                throw new ArgumentNullException(nameof(usuarioDto));
            }
            Usuario usuario = new()
            {
                UserName = usuarioDto.Email,
                Email = usuarioDto.Email,                
                Estado = true
            };
            var resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);


            if (resultado.Errors.Any())
                return "ErrorPassword";

            if (resultado.Succeeded)
            {
                return usuario.Id;
            }
            return null;

        }
    }
}
