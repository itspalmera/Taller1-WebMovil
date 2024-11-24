using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
    /// Represents a purchase made by a user for a product.
    /// </summary>
    [PrimaryKey(nameof(productId), nameof(userId), nameof(purchaseReceiptId))]
    public class Purchase
    {
        /// <summary>
        /// Gets or sets the quantity of the product purchased.
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Gets or sets the total price of the purchase.
        /// </summary>
        public int totalPrice { get; set; }

        // Relationships

        /// <summary>
        /// Gets or sets the product ID that was purchased.
        /// </summary>
        public int productId { get; set; }

        /// <summary>
        /// Gets or sets the product that was purchased.
        /// </summary>
        public Product product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user ID who made the purchase.
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Gets or sets the user who made the purchase.
        /// </summary>
        public User user { get; set; } = null!;

        /// <summary>
        /// Gets or sets the purchase receipt ID for this purchase.
        /// </summary>
        public int purchaseReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the purchase receipt associated with this purchase.
        /// </summary>
        public PurchaseReceipt purchaseReceipt { get; set; } = null!;
    }
}