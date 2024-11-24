using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// Retrieves all items in the shopping cart for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user whose shopping cart items are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of shopping cart items.</returns>
        Task<IEnumerable<ShoppingCartInfoDto>> GetCartItems(string userId);

        /// <summary>
        /// Retrieves a specific cart item by product for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart item is to be retrieved.</param>
        /// <param name="productId">The ID of the product to find in the shopping cart.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the cart item, or null if it doesn't exist.</returns>
        Task<CartItem?> GetCartItemByProduct(string userId, int productId);

        /// <summary>
        /// Deletes a specific cart item for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart item is to be deleted.</param>
        /// <param name="productId">The ID of the product to delete from the shopping cart.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the delete operation was successful.</returns>
        Task<bool> DeleteCartItem(string userId, int productId);
    }
}