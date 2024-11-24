using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Products;

namespace Taller1_WebMovil.Src.DTOs.Purchase
{
    /// <summary>
    /// Data Transfer Object (DTO) representing purchase information for the client.
    /// </summary>
    public class PurchaseInfoClientDto
    {
        /// <summary>
        /// Gets or sets the date of the purchase.
        /// </summary>
        /// <value>
        /// A string representing the date the purchase was made, typically in the format "dd/MM/yyyy".
        /// </value>
        public string purchaseDate { get; set; }

        /// <summary>
        /// Gets or sets the total price of the purchase.
        /// </summary>
        /// <value>
        /// An integer representing the total price of the purchase.
        /// </value>
        public int totalPurchasePrice { get; set; }

        /// <summary>
        /// Gets or sets the list of products included in the purchase.
        /// </summary>
        /// <value>
        /// A list of <see cref="ProductInfoClientDto"/> representing the details of the products in the purchase.
        /// </value>
        public List<ProductInfoClientDto> ProductDetails { get; set; } = new List<ProductInfoClientDto>();
    }
}