using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Dtos.Roles
{
    public class CrearRolDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(30, ErrorMessage = "Ingrese mínimo 5 caracteres y máximo 30", MinimumLength = 5)]
        [Display(Name = "Nombre del rol")]
        public string Rol { get; set; }
    }
}
