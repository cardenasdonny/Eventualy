using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Model.Entities
{
    public class TipoDocumento
    {
        [Key]
        public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Cliente> Clientes { get; set; }
    }
}
