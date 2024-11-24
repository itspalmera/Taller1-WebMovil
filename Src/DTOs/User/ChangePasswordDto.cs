using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) for changing the user's password.
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Gets or sets the current password of the user.
        /// </summary>
        /// <value>
        /// A string representing the user's current password.
        /// </value>
        [Required(ErrorMessage = "El campo confirmar contraseña es obligatorio.")]
        public string currentPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password that the user wants to set.
        /// </summary>
        /// <value>
        /// A string representing the new password.
        /// </value>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public string newPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        /// <value>
        /// A string representing the confirmation of the new password.
        /// </value>
        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio.")]
        [Compare("newPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string confirmPassword { get; set; } = string.Empty;
    }
}