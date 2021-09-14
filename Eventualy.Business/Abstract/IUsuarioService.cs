using Eventualy.Business.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Abstract
{
    public interface IUsuarioService
    {
        Task<string> CrearUsuario(UsuarioDto usuarioDto);
        Task<UsuarioResumenDto> ObtenerUsuarioDtoPorEmail(string email);
        Task<IEnumerable<UsuarioResumenDto>> ObtenerListaUsuarios();
    }
}
