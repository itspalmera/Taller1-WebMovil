using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Validators;

namespace Taller1_WebMovil.Src.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object (DTO) for registering a new user.
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        /// Gets or sets the user's Rut (unique identification number).
        /// </summary>
        /// <value>
        /// A string representing the user's Rut. It must be a valid format.
        /// </value>
        [Required(ErrorMessage = "El campo Rut es obligatorio.")]
        [RutValidation(ErrorMessage = "El Rut ingresado no es válido.")]
        public string rut { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// A string representing the user's full name. It must be at least 8 characters long.
        /// </value>
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MinLength(8, ErrorMessage = "El nombre tiene que tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre tiene que tener máximo 255 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El nombre solo puede contener caracteres del abecedario español.")]
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's birthdate.
        /// </summary>
        /// <value>
        /// A string representing the user's birthdate. The user must be of legal age.
        /// </value>
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        [AgeValidation]
        public string birthDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>
        /// A string representing the user's email. It must be in a valid email format.
        /// </value>
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email ingresado no tiene un formato válido.")]
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's gender identifier.
        /// </summary>
        /// <value>
        /// A string representing the user's gender. It must match an allowed gender.
        /// </value>
        [Required(ErrorMessage = "El campo Género es obligatorio.")]
        [GenderValidation]
        public string genderId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <value>
        /// A string representing the user's password. It must be alphanumeric, between 8 and 20 characters long.
        /// </value>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public string password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        /// <value>
        /// A string representing the confirmation password. It must match the user's password.
        /// </value>
        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio.")]
        [Compare("password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}