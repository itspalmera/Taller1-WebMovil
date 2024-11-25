using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Products
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing product information for the client.
    /// </summary>
    public class ProductInfoClientDto
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// A string representing the name of the product.
        /// </value>
        public string nameProduct { get; set; }

        /// <summary>
        /// Gets or sets the type of the product (e.g., category or type of item).
        /// </summary>
        /// <value>
        /// A string representing the type of the product.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        /// <value>
        /// A string representing the price of the product.
        /// </value>
        public string price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product available or purchased.
        /// </summary>
        /// <value>
        /// A string representing the quantity of the product.
        /// </value>
        public string quantity { get; set; }

        /// <summary>
        /// Gets or sets the total price for the quantity of the product.
        /// </summary>
        /// <value>
        /// A string representing the total price calculated from the quantity and price.
        /// </value>
        public string totalPrice { get; set; }
    }
}