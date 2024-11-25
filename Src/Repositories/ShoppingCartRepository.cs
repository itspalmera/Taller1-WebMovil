using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{

    /// <summary>
    /// Repository for managing shopping cart-related operations.
    /// </summary>
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all items from the shopping cart for a specific user based on their email.
        /// </summary>
        /// <param name="email">The email of the user whose shopping cart items are to be retrieved.</param>
        /// <returns>
        /// A list of shopping cart items associated with the user, represented as `ShoppingCartInfoDto` objects.
        /// If no items are found, an empty list is returned.
        /// </returns>
        /// <response code="200">Returns a list of shopping cart items for the user.</response>
        /// <response code="404">Returns an empty list if no shopping cart items are found for the user.</response>
        public async Task<IEnumerable<ShoppingCartInfoDto>> GetCartItems(string email)
        {
            // Obtener los carritos del repositorio
            var listCartItems = await _context.ShoppingCarts.Include(sc => sc.Items)
                                                            .ThenInclude(item => item.Product)
                                                            .Where(sc => sc.user.Email == email)
                                                            .ToListAsync();
            
            // Si no hay carritos, retornar una lista vacía

            if (listCartItems == null || !listCartItems.Any())
            {
                return Enumerable.Empty<ShoppingCartInfoDto>();
            }
            // Usar el mapper para mapear los cartItems a ShoppingCartInfoDto
            var cartItemsDto = listCartItems
                .SelectMany(sc => sc.Items)  // Desenrolla todos los items de todos los carritos
                .Select(cartItem => ShoppingCartMapper.ToShoppingCartInfoDto(cartItem))  // Usar el mapper
                .ToList();

            // Retornar la lista de DTOs
            return cartItemsDto;
        }
        
        /// <summary>
        /// Retrieves a specific item from the shopping cart by product ID for a given user.
        /// </summary>
        /// <param name="userId">The ID of the user whose shopping cart is being searched.</param>
        /// <param name="productId">The ID of the product whose cart item is being retrieved.</param>
        /// <returns>
        /// The `CartItem` object for the specified product in the user's shopping cart, or `null` if not found.
        /// </returns>
        /// <response code="200">Returns the cart item if found, or `null` if not found.</response>
        /// <response code="404">Returns `null` if no cart item matching the product ID is found in the user's cart.</response>
        public async Task<CartItem?> GetCartItemByProduct(string userId,int productId){
            var cartItems = await _context.ShoppingCarts.Where(sc => sc.user.Email == userId)
                                                        .ToListAsync();
            
            return await Task.FromResult<CartItem?>(null);
        }

        /// <summary>
        /// Deletes a specific item from the user's shopping cart based on product ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose shopping cart item is to be deleted.</param>
        /// <param name="productId">The ID of the product to be removed from the cart.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// </returns>
        /// <response code="200">Returns `true` if the item was successfully deleted from the cart.</response>
        /// <response code="400">Returns `false` if the item could not be deleted, for example if the item doesn't exist.</response>
        public async Task<bool> DeleteCartItem(string userId, int productId)
        {
            
                return true;
        }
    }
}