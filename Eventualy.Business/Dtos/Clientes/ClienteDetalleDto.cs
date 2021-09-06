using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Dtos.Clientes
{
    public class ClienteDetalleDto
    {   
        public string Nombres { get; set; }        
        public string Email { get; set; }        
        public string Documento { get; set; }
        public string Estado { get; set; }        
        public string TipoDocumento { get; set; }
        public int Edad { get; set; }
        public DateTime Cumpleanios { get; set; }
    }
}
