using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "El campo confirmar contraseña es obligatorio.")]
        public string currentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public string newPassword {get; set;} = string.Empty;

        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio.")]
        [Compare("newPassword", ErrorMessage ="Las contraseñas no coinciden.")]
        public string confirmPassword {get; set;} = string.Empty;
    }
}