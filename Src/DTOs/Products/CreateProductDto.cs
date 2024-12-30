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
        [MinLength(10), MaxLength(64), RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El nombre debe ser alfabético.")]
public string name { get; set; }

         /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Required]
        [Range(1, 100000000, ErrorMessage = "El precio debe estar entre 1 y 99,999,999.")] 
        public int price { get; set; }

         /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        [Required]
        [Range(1, 100000)]
        public int stock { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        [Required]
        [RegularExpression(@".*\.(jpg|png)$", ErrorMessage = "Solo se permiten imágenes .jpg o .png.")]
        public string image {get; set;}

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        public String categoryName { get; set; }
    } 
}