using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Dtos.Usuarios
{
    public class ResetearPasswordDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña", Order = -9,
        Prompt = "Ingrese la contraseña", Description = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "El {0} debe tener al menos {2} y maximo {1} caracteres.", MinimumLength = 9)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña", Order = -9,
        Prompt = "Confirme la contraseña", Description = "Confirme la contraseña")]
        [Compare("Password",
            ErrorMessage = "El Password y confirmar password debe coincidir")]
        public string ConfirmarPassword { get; set; }

        public string Token { get; set; }

    }
}
