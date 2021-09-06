using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Model.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }        
        public string Nombres { get; set; }        
        public string Email { get; set; }        
        public string Documento { get; set; }
        public bool Estado { get; set; }               
        public int TipoDocumentoId { get; set; }
        public int Edad { get; set; }
        public DateTime Cumpleanios { get; set; }
        public virtual TipoDocumento TiposDocumento { get; set; }
    }
}
