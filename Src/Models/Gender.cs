using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
    /// Represents a gender entity used for categorizing users or individuals.
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Gets or sets the unique identifier for the gender.
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name of the gender.
        /// </summary>
        [Required]
        public string name { get; set; }
    }
}