using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
    /// Represents a product entity in the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity available for the product.
        /// </summary>
        public int stock { get; set; }

        /// <summary>
        /// Gets or sets the image URL or path for the product.
        /// </summary>
        [Required]
        public string image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is enabled (available for sale).
        /// </summary>
        public bool enabled { get; set; }

        // Relationships
        /// <summary>
        /// Gets or sets the category ID that the product belongs to.
        /// </summary>
        public int categoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public Category category { get; set; } = null!;
    }
}