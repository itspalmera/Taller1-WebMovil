using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object (DTO) for user login.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        /// <value>
        /// A non-empty string representing the user's email address.
        /// </value>
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <value>
        /// A non-empty string representing the user's password.
        /// </value>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string password { get; set; } = string.Empty;
    }
}