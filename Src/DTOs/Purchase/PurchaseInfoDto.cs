using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Purchase
{
    /// <summary>
    /// Data Transfer Object (DTO) representing detailed purchase information.
    /// </summary>
    public class PurchaseInfoDto
    {
        /// <summary>
        /// Gets or sets the date of the purchase.
        /// </summary>
        /// <value>
        /// A string representing the date when the purchase was made, typically in the format "dd/MM/yyyy".
        /// </value>
        public string purchaseDate { get; set; }

        /// <summary>
        /// Gets or sets the receipt ID of the purchase.
        /// </summary>
        /// <value>
        /// A string representing the unique identifier for the purchase receipt.
        /// </value>
        public string idReceiptPurchase { get; set; }

        /// <summary>
        /// Gets or sets the Rut (ID number) of the user who made the purchase.
        /// </summary>
        /// <value>
        /// A string representing the Rut (Chilean identification number) of the user.
        /// </value>
        public string rut { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who made the purchase.
        /// </summary>
        /// <value>
        /// A string representing the name of the user who made the purchase.
        /// </value>
        public string nameUser { get; set; }

        /// <summary>
        /// Gets or sets the total price of the purchase.
        /// </summary>
        /// <value>
        /// A string representing the total price of the purchase.
        /// </value>
        public string totalPrice { get; set; }
    }
}