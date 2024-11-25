using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    /// <summary>
    /// Represents a receipt for a purchase transaction.
    /// </summary>
    public class PurchaseReceipt
    {
        /// <summary>
        /// Gets or sets the unique identifier for the purchase receipt.
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the country where the purchase was made.
        /// </summary>
        [Required]
        public string country { get; set; }

        /// <summary>
        /// Gets or sets the city where the purchase was made.
        /// </summary>
        [Required]
        public string city { get; set; }

        /// <summary>
        /// Gets or sets the district where the purchase was made.
        /// </summary>
        [Required]
        public string district { get; set; }

        /// <summary>
        /// Gets or sets the street where the purchase was made.
        /// </summary>
        [Required]
        public string street { get; set; }

        /// <summary>
        /// Gets or sets the purchase date.
        /// </summary>
        public DateOnly purchaseDate { get; set; }

        /// <summary>
        /// Gets or sets the total price of the purchase recorded in the receipt.
        /// </summary>
        public int totalPrice { get; set; }
    }
}