using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Dtos.Usuarios
{
    public class UsuarioResumenDto
    {
        public string UsuarioId { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public string Rol { get; set; }
    }
}
