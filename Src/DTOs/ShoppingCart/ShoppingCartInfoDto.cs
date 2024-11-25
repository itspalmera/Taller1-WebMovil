using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.ShoppingCart
{
    /// <summary>
    /// Data Transfer Object (DTO) representing detailed information about an item in a shopping cart.
    /// </summary>
    public class ShoppingCartInfoDto
    {
        /// <summary>
        /// Gets or sets the identifier for the product in the shopping cart.
        /// </summary>
        /// <value>
        /// A string representing the unique identifier for the product.
        /// </value>
        public string productId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product in the shopping cart.
        /// </summary>
        /// <value>
        /// A string representing the name of the product.
        /// </value>
        public string productName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the shopping cart.
        /// </summary>
        /// <value>
        /// A string representing the quantity of the product in the cart.
        /// </value>
        public string quantity { get; set; }
    }
}