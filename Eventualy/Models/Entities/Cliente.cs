using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eventualy.WEB.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }


        [Required(ErrorMessage = "El nombre del cliente es requerido ")]
        [StringLength(30, ErrorMessage = "Debe contener entre 5 y 30 caracteres", MinimumLength = 5)]
        [Display(Name = "Nombre del cliente")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El email del cliente es requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El documento es requerido")]
        [Range(9999, 999999999999999, ErrorMessage = "El documento es inválido")]
        public string Documento { get; set; }
        public bool Estado { get; set; }

        public virtual TipoDocumento TiposDocumento { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido")]
        //[ForeignKey("TiposDocumento")]
        [Display(Name = "Tipo de documento")]
        public int? TipoDocumentoId { get; set; }
    }
}
