using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.ShoppingCart
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a shopping cart.
    /// </summary>
    public class ShoppingCartDto
    {
        /// <summary>
        /// Gets or sets the user identifier associated with the shopping cart.
        /// </summary>
        /// <value>
        /// A string representing the unique identifier of the user who owns the shopping cart.
        /// </value>
        public string userId { get; set; }

        /// <summary>
        /// Gets or sets the identifier for a specific cart item.
        /// </summary>
        /// <value>
        /// An integer representing the unique identifier for a cart item within the shopping cart.
        /// </value>
        public int cartItemId { get; set; }
    }
}