using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Taller1_WebMovil.Src.Models
{
    
    /// <summary>
    /// Represents a shopping cart entity in the system.
    /// </summary>
    public class ShoppingCart
    {

        /// <summary>
        /// Gets or sets the unique identifier for the shopping cart.
        /// </summary>
        [Key]
        public int id {get; set;}


        //Relaciones

        /// <summary>
        /// Gets or sets the unique identifier of the user who owns the shopping cart.
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the shopping cart.
        /// </summary>
        public User user { get; set; }= null!;

        /// <summary>
        /// Gets or sets the list of items in the shopping cart.
        /// </summary>

        public List<CartItem> Items { get; set; } = new();
    }
}