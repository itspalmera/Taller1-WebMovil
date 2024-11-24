using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
/// Represents a user in the system, inheriting from IdentityUser to include authentication features.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Gets or sets the RUT (Unique Identifier) of the user. This is typically used for identification in certain countries.
    /// </summary>
    public string rut { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's birthdate.
    /// </summary>
    public DateOnly birthDate { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the user is enabled or not.
    /// </summary>
    public bool enable { get; set; }

    // Relationships

    /// <summary>
    /// Gets or sets the ID of the user's gender.
    /// </summary>
    public int genderId { get; set; }

    /// <summary>
    /// Gets or sets the gender of the user.
    /// </summary>
    public Gender gender { get; set; } = null!;
}
}