using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing an item in the shopping cart.
    /// </summary>
    public class CartItemDto
    {
        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        /// <value>
        /// An integer representing the quantity of the product in the cart. 
        /// It must be a positive number.
        /// </value>
        public int quantity { get; set; }

        /// <summary>
        /// Gets or sets the product identifier associated with the cart item.
        /// </summary>
        /// <value>
        /// An integer representing the unique identifier of the product.
        /// </value>
        public int productId { get; set; }
    }
}