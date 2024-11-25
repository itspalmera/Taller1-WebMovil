using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs;

namespace Taller1_WebMovil.Src.Interface
{

    /// <summary>
    /// Interface for managing shopping cart items.
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        /// Adds an item to the shopping cart.
        /// </summary>
        /// <param name="email">The email of the user, used to identify their shopping cart.</param>
        /// <param name="cartItemDto">The data transfer object (DTO) representing the item to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> AddToCart(string email, CartItemDto cartItemDto);

        /// <summary>
        /// Deletes an item from the shopping cart.
        /// </summary>
        /// <param name="email">The email of the user, used to identify their shopping cart.</param>
        /// <param name="productId">The ID of the product to remove from the cart.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Task<bool> DeleteCartItem(string email, int productId);
    }
}