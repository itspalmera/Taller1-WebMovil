using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Validators;

namespace Taller1_WebMovil.Src.DTOs.User
{
    public class EditUserDto
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MinLength(8, ErrorMessage = "El nombre tiene que tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre tiene que tener máximo 255 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El nombre solo puede contener caracteres del abecedario español.")]
        public string name {get; set;} = string.Empty;
        
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        [AgeValidation]
        public string birthDate {get; set;} = string.Empty;

        [Required(ErrorMessage = "El campo Género es obligatorio.")]
        [GenderValidation]
        public string genderId {get; set;} = string.Empty;
    }
}