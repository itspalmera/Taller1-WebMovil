using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing shopping cart-related operations.
    /// </summary>
    public interface IShoppingCartService
    {

        /// <summary>
        /// Retrieves all items in the shopping cart for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose shopping cart items are to be retrieved.</param>
        /// <returns>
        /// A list of items in the user's shopping cart, represented as <see cref="ShoppingCartInfoDto"/> objects.
        /// </returns>
        Task<IEnumerable<ShoppingCartInfoDto?>> GetCartItems(string userId);
        
        /// <summary>
        /// Deletes a specific item from the user's shopping cart.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart item is to be deleted.</param>
        /// <param name="id">The ID of the product to be removed from the cart.</param>
        /// <returns>
        /// A boolean indicating whether the item was successfully removed from the cart.
        /// </returns>
        Task<bool> DeleteCartItem(string userId,int id);
    }
}