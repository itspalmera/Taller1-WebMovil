using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a product to the user's shopping cart. If the product is already in the cart, it increments the quantity.
        /// If the quantity is less than 1 after incrementing, the item is removed from the cart.
        /// </summary>
        /// <param name="email">The email of the user whose cart the item will be added to.</param>
        /// <param name="cartItemDto">The details of the item to be added to the cart, including product ID and quantity.</param>
        /// <returns>
        /// Returns true if the product was successfully added to the cart or if the cart was updated, false if the product or user is not found.
        /// </returns>
        /// <response code="200">Returns true if the product was successfully added or updated in the cart.</response>
        /// <response code="400">Returns false if the product or user is not found.</response>
        public async Task<bool> AddToCart(string email, CartItemDto cartItemDto)
        {

            // Buscar el producto en la base de datos
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.id == cartItemDto.productId);
            if (product == null)
            {
                return false;
            }
            // Buscar el carrito del usuario
            ShoppingCart shoppingCart = await _context.ShoppingCarts.Include(c => c.Items)
                                                                    .FirstOrDefaultAsync(c => c.user.Email == email);
            // Si el carrito no existe, crearlo
            if (shoppingCart == null)
            {
                User user = await _context.Users.Where(u=>u.Email == email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }
                shoppingCart = new ShoppingCart
                {
                    userId = user.Id,
                    user = user,
                    Items = new List<CartItem>()
                };

                _context.ShoppingCarts.Add(shoppingCart);
            }
            // Verificar si el producto ya está en el carrito
            CartItem existingItem = shoppingCart.Items.FirstOrDefault(i => i.productId == cartItemDto.productId);

            if (existingItem != null)
            {
                // Si el producto ya está en el carrito, incrementar la cantidad
                if(existingItem.quantity+cartItemDto.quantiy <1)
                // Eliminar el item del carrito si la cantidad es menor que 1
                shoppingCart.Items.Remove(existingItem);
                else{
                existingItem.quantity += cartItemDto.quantiy;
                }
            }
            else
            {
                 // Si el producto no está en el carrito, agregarlo como un nuevo ítem
                CartItem newItem = new CartItem
                {
                    quantity = cartItemDto.quantiy,
                    price = product.price, // Precio actual del producto
                    Product = product,
                    ShoppingCart = shoppingCart
                };

                shoppingCart.Items.Add(newItem);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes an item from the user's shopping cart based on the product ID.
        /// </summary>
        /// <param name="email">The email of the user whose cart the item will be removed from.</param>
        /// <param name="productId">The ID of the product to be removed from the cart.</param>
        /// <returns>
        /// Returns true if the item was successfully removed from the cart, false if the cart or product is not found.
        /// </returns>
        /// <response code="200">Returns true if the item was successfully removed from the cart.</response>
        /// <response code="400">Returns false if the cart or product is not found.</response>
        public async Task<bool> DeleteCartItem(string email, int productId){
            // Buscar el carrito del usuario por su email
        var shoppingCart = await _context.ShoppingCarts
                                        .Include(sc => sc.Items)
                                        .ThenInclude(item => item.Product)
                                        .FirstOrDefaultAsync(sc => sc.user.Email == email);

        if (shoppingCart == null)
        {
            // Si el carrito no existe, devolver false
            return false;
        }

        // Buscar el item del carrito que coincide con el productId
        var cartItem = shoppingCart.Items.FirstOrDefault(item => item.productId == productId);

        if (cartItem == null)
        {
            // Si no se encuentra el item con ese productId, devolver false
            return false;
        }

        // Eliminar el item del carrito
        shoppingCart.Items.Remove(cartItem);

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();

        // Retornar true si el item fue eliminado correctamente
        return true;
        }   
    }
}