using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
    /// Represents an item in a shopping cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart item.
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the product in the cart.
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated shopping cart.
        /// </summary>
        public int shoppingCartId { get; set; }

        /// <summary>
        /// Gets or sets the associated shopping cart.
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the associated product.
        /// </summary>
        public int productId { get; set; }

        /// <summary>
        /// Gets or sets the associated product.
        /// </summary>
        public Product Product { get; set; } = null!;
    }
}