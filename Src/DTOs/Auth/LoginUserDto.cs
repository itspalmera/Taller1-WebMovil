using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Auth
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        public string email {get; set;} = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string password {get; set;} = string.Empty;
    }
}