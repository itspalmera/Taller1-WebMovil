using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs.Products
{

    /// <summary>
    /// Data Transfer Object (DTO) for creating a new product.
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        public string name { get; set; }

         /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Required]
        public int price { get; set; }

         /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        [Required]
        public int stock { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        [Required]
        public string image {get; set;}

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public int categoryId { get; set; }
    }
}