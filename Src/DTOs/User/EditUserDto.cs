using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Validators;

namespace Taller1_WebMovil.Src.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) used for editing user information.
    /// </summary>
    public class EditUserDto
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// A string representing the user's name. It must contain at least 8 characters and at most 255 characters.
        /// It can only contain Spanish alphabet characters (letters and spaces).
        /// </value>
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MinLength(8, ErrorMessage = "El nombre tiene que tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre tiene que tener máximo 255 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El nombre solo puede contener caracteres del abecedario español.")]
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's birth date.
        /// </summary>
        /// <value>
        /// A string representing the user's birth date, which should be validated according to age requirements.
        /// </value>
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        [AgeValidation]
        public string birthDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        /// <value>
        /// A string representing the user's gender. It must be validated with the gender-specific logic.
        /// </value>
        [Required(ErrorMessage = "El campo Género es obligatorio.")]
        [GenderValidation]
        public string genderId { get; set; } = string.Empty;
    }
}