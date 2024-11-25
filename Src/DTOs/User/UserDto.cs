using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) used for transferring user information.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the user's Rut (Identification number).
        /// </summary>
        /// <value>
        /// A string representing the user's Rut. This value is mandatory and uniquely identifies the user.
        /// </value>
        public string rut { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// A string representing the user's name. This value cannot be null or empty.
        /// </value>
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's birth date.
        /// </summary>
        /// <value>
        /// A string representing the user's birth date in a specific format (e.g., "dd/MM/yyyy").
        /// </value>
        public string birthDate { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>
        /// A string representing the user's email. It must be a valid email address format.
        /// </value>
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        /// <value>
        /// A string representing the user's gender. This field will typically contain a gender identifier.
        /// </value>
        public string gender { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user's account status (enabled or disabled).
        /// </summary>
        /// <value>
        /// A string representing the user's status, where "enabled" indicates an active account and "disabled" indicates an inactive account.
        /// </value>
        public string enable { get; set; }
    }
}