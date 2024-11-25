using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{

    /// <summary>
    /// Service implementation for managing shopping cart-related operations.
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
    private readonly IShoppingCartRepository _shoppingCartRepository;


    /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartService"/> class.
        /// </summary>
        /// <param name="shoppingCartRepository">The repository for handling shopping cart data.</param>
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository){
            
            _shoppingCartRepository = shoppingCartRepository;
        }


        /// <summary>
        /// Deletes a specific item from the user's shopping cart.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart item is to be deleted.</param>
        /// <param name="id">The ID of the product to be removed from the cart.</param>
        /// <returns>
        /// A boolean indicating whether the item was successfully removed from the cart.
        /// </returns>
        public async Task<bool> DeleteCartItem(string userId, int id)
        {
            var result = await _shoppingCartRepository.DeleteCartItem(userId,id);
            return result;
        }


        /// <summary>
        /// Retrieves all items in the shopping cart for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart items are to be retrieved.</param>
        /// <returns>
        /// A list of items in the shopping cart, represented as <see cref="ShoppingCartInfoDto"/> objects.
        /// </returns>
        public async Task<IEnumerable<ShoppingCartInfoDto?>> GetCartItems(string userId)
        {
            var listCartItems = await _shoppingCartRepository.GetCartItems(userId);
            return listCartItems;
        }
    }
}